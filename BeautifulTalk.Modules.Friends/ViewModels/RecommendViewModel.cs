using BeautifulDB.Entities;
using BeautifulDB.Helpers;
using BeautifulTalk.Modules.Friends.Converters;
using BeautifulTalk.Modules.Friends.Models;
using BeautifulTalk.Modules.Friends.Services;
using BeautifulTalkInfrastructure.AliveInformation;
using BeautifulTalkInfrastructure.Logger;
using BeautifulTalkInfrastructure.ProtocolFormat;
using BeautifulTalkInfrastructure.PubSubEvents;
using BeautifulTalkInfrastructure.RegionNames;
using BeautifulTalkInfrastructure.ViewNames;
using CommonControl.BusyIndicator;
using CommonControl.Navigation;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace BeautifulTalk.Modules.Friends.ViewModels
{
    public class RecommendViewModel : BindableBase, INavigationAware
    {
        private readonly ILoggerFacade m_Logger;
        private readonly IRegionManager m_RegionManager;
        private ICollectRecommendUsers m_CollectRecommendUsers;
        private IAddFriendService m_AddFriendService;
        private IGetUserInfoService m_GetUserInfoService;
        private IFriendsMainViewModel m_FriendsMainViewModel;
        private RecommendFriendSummaryCollection m_RecommendFriends;

        public RecommendFriendSummaryCollection RecommendFriends
        {
            get { return this.m_RecommendFriends; }
            set { SetProperty(ref this.m_RecommendFriends, value); }
        }
        public DelegateCommand<string> AddFriendCommand { get; private set; }
        public DelegateCommand<SmoothBusyIndicator> RefreshCommand { get; private set; }
        public DelegateCommand NavigateToBackCommand { get; private set; }
        public RecommendViewModel(ILoggerFacade logger, IRegionManager regionManager, IEventAggregator eventAggregator, 
            ICollectRecommendUsers collectRecommendUsers, IAddFriendService addFriendService, IGetUserInfoService getUserInfoService)
        {
            if (null == logger) throw new ArgumentNullException("logger");
            if (null == regionManager) throw new ArgumentNullException("regionManager");
            if (null == eventAggregator) throw new ArgumentNullException("eventAggregator");
            if (null == collectRecommendUsers) throw new ArgumentNullException("collectRecommendUsers");
            if (null == addFriendService) throw new ArgumentNullException("addFriendService");
            if (null == getUserInfoService) throw new ArgumentNullException("getUserInfoService");

            this.m_Logger = logger;
            this.m_RegionManager = regionManager;
            this.m_CollectRecommendUsers = collectRecommendUsers;
            this.m_AddFriendService = addFriendService;
            this.m_GetUserInfoService = getUserInfoService;

            this.AddFriendCommand = new DelegateCommand<string>(ExecuteAddFriendCommand);
            this.NavigateToBackCommand = new DelegateCommand(ExecuteNavigateToBackCommand);
            this.RefreshCommand = new DelegateCommand<SmoothBusyIndicator>(ExecuteRefreshCommand);
            this.RecommendFriends = new RecommendFriendSummaryCollection();

            Task.Run(() => { this.UpdateRecommendUsers(); });
        }

        private void ExecuteRefreshCommand(SmoothBusyIndicator busyIndicator)
        {
            busyIndicator.IsBusy = true;

            Task.Run(() => 
            {
                this.UpdateRecommendUsers();

                Thread.Sleep(1000);
                busyIndicator.IsBusy = false;
            });
        }
        private void ExecuteAddFriendCommand(string strRecommendUserSid)
        {
            if (true == string.IsNullOrEmpty(strRecommendUserSid)) return;

            Task.Run(() =>
            {
                var TargetFriend = this.m_RecommendFriends.FirstOrDefault(r => r.UserSID == strRecommendUserSid);
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new ThreadStart(() =>
                {
                    RecommendFriends.Remove(TargetFriend);
                }));

                if (true == this.m_AddFriendService.AddFriend(AuthRepository.MQKeyInfo.UserSid, strRecommendUserSid))
                {
                    try
                    {
                        var AddFriend = this.m_GetUserInfoService.GetUserInfo(strRecommendUserSid);

                        if (null != AddFriend)
                        {
                            var UserCollection = ConnectionHelper.DB.GetCollection<UserEntity>("UserEntity");
                            var FindUserQuery = Query<UserEntity>.EQ(u => u.Sid, strRecommendUserSid);
                            var FindedUser = UserCollection.FindOne(FindUserQuery);

                            if (null == FindedUser) { UserCollection.Save(AddFriend); }
                        }

                        var FindFriendQuery = Query<FriendsEntity>.EQ(u => u.UserSID, AuthRepository.MQKeyInfo.UserSid);
                        var FriendsCollection = ConnectionHelper.DB.GetCollection<FriendsEntity>("FriendsEntity");
                        var FindedFriend = FriendsCollection.FindOne(FindFriendQuery);

                        if (null != FindedFriend)
                        {
                            if (false == FindedFriend.FriendSIDs.Contains(strRecommendUserSid))
                            {
                                FindedFriend.FriendSIDs.Add(strRecommendUserSid);
                                var UpdateQuery = Update<FriendsEntity>.Set(f => f.FriendSIDs, FindedFriend.FriendSIDs);
                                FriendsCollection.Update(FindFriendQuery, UpdateQuery);

                                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new ThreadStart(() =>
                                {
                                    this.m_FriendsMainViewModel.Friends.Add(new Friend(AddFriend.ThumbnailPath, AddFriend.UserId, AddFriend.Sid, AddFriend.NickName, AddFriend.Comment));
                                }));
                            }
                        }
                    }
                    catch (Exception unExpectedException)
                    {
                        GlobalLogger.Log(unExpectedException.Message);
                    }
                }
            });
        }

        private void ExecuteNavigateToBackCommand()
        {
            var FriendsViewUri = new Uri(FriendsViewNames.FriendsView, UriKind.Relative);
            this.m_RegionManager.RequestNavigate(FriendsRegionNames.NavigationRegion, FriendsViewUri);
        }

        private void UpdateRecommendUsers()
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new ThreadStart(() =>
            {
                this.RecommendFriends.Clear();
            }));
            
            this.m_CollectRecommendUsers.Collect(20, AuthRepository.MQKeyInfo.UserSid, this.RecommendFriends);
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            try
            {
                this.m_FriendsMainViewModel = navigationContext.Parameters[typeof(FriendsViewModel).GetHashCode().ToString()] as IFriendsMainViewModel;
                this.m_FriendsMainViewModel.Friends.CollectionChanged -= Friends_CollectionChanged;
                this.m_FriendsMainViewModel.Friends.CollectionChanged += Friends_CollectionChanged;
            }
            catch (ArgumentNullException argsNullException)
            {
                GlobalLogger.Log(argsNullException.Message);
            }
            catch (NullReferenceException nullRefException)
            {
                GlobalLogger.Log(nullRefException.Message);
            }
            catch (InvalidCastException invalidCastException)
            {
                GlobalLogger.Log(invalidCastException.Message);
            }
        }

        private void Friends_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (NotifyCollectionChangedAction.Add == e.Action)
            {
                this.m_FriendsMainViewModel.Friends.TotalUnReadCount += 1;
            }
        }
    }
}
