﻿using BeautifulDB.Entities;
using BeautifulDB.Helpers;
using BeautifulTalk.Modules.Friends.Models;
using BeautifulTalk.Modules.Friends.Services;
using BeautifulTalkInfrastructure.AliveInformation;
using BeautifulTalkInfrastructure.Interfaces;
using BeautifulTalkInfrastructure.ProtocolFormat;
using BeautifulTalkInfrastructure.PubSubEvents;
using BeautifulTalkInfrastructure.RegionNames;
using BeautifulTalkInfrastructure.ViewNames;
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
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace BeautifulTalk.Modules.Friends.ViewModels
{
    public class FriendsViewModel : BindableBase, INavigationAware
    {
        private FriendCollection m_Friends;
        private readonly ILoggerFacade m_Logger;
        private readonly IRegionManager m_RegionManager;
        private readonly IEventAggregator m_EventAggregator;
        private readonly ICollectFriendsService m_CollectFriendsService;
        private IGetRoomInfoService m_GetRoomInfoService;
        public DelegateCommand NavigateToForwardCommand { get; private set; }
        public DelegateCommand<Friend> ItemDoubleClickedCommand { get; private set; }
        public FriendCollection Friends 
        {
            get { return this.m_Friends; }
            set { SetProperty(ref this.m_Friends, value); }
        }
        public FriendsViewModel(ILoggerFacade logger, IRegionManager regionManager, IEventAggregator eventAggregator, 
            ICollectFriendsService collectFriendsService, IGetRoomInfoService getRoomInfoService)
        {
            if (null == logger) throw new ArgumentNullException("logger");
            if (null == regionManager) throw new ArgumentNullException("regionManager");
            if (null == eventAggregator) throw new ArgumentNullException("eventAggregator");
            if (null == collectFriendsService) throw new ArgumentNullException("collectFriendsService");
            if (null == getRoomInfoService) throw new ArgumentNullException("getRoomInfoService");

            this.m_Logger = logger;
            this.m_RegionManager = regionManager;
            this.m_EventAggregator = eventAggregator;
            this.m_CollectFriendsService = collectFriendsService;
            this.m_GetRoomInfoService = getRoomInfoService;

            this.NavigateToForwardCommand = new DelegateCommand(ExecuteNavigateToForwardCommand);
            this.ItemDoubleClickedCommand = new DelegateCommand<Friend>(ExecuteItemDoubleClickedCommand);
            this.Friends = new FriendCollection();

            Task.Run(() => m_CollectFriendsService.CollectFriends(AuthRepository.MQKeyInfo.UserSid, this.Friends));
        }

        private void ExecuteItemDoubleClickedCommand(Friend friend)
        {
            if (null != friend)
            {
                IList<string> arMembers = new List<string>() { friend.Sid, AuthRepository.MQKeyInfo.UserSid };
                var RoomInfo = this.m_GetRoomInfoService.GetRoomInfo(arMembers);

                if (null != RoomInfo)
                {
                    var RoomCollection = ConnectionHelper.DB.GetCollection<RoomEntity>("RoomEntity");
                    var FindRoomQuery = Query<RoomEntity>.EQ(r => r.Sid, RoomInfo.Sid);
                    var FindedRoom = RoomCollection.FindOne(FindRoomQuery);

                    if (null == FindedRoom) 
                    {
                        RoomInfo.UserSid = AuthRepository.MQKeyInfo.UserSid;
                        RoomCollection.Save(RoomInfo); 
                    }

                    var Parameters  = new FriendDoubleClickParams(RoomInfo.Sid, RoomInfo.ActiveMemberSids, RoomInfo.UnReadMsgCount,
                        RoomInfo.LastMsgSummary, RoomInfo.LastMsgDate, RoomInfo.ThumbnailPath);

                    this.m_EventAggregator.GetEvent<FriendDoubleClickedEvent>().Publish(Parameters);
                }
            }
        }

        public void LoadFriends()
        {
            Task.Run(() =>
            {
                this.m_CollectFriendsService.CollectFriends(AuthRepository.MQKeyInfo.UserSid, this.Friends);
            });
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
            var AddedFriends = (List<RecommendFriendSummary>)navigationContext.Parameters[typeof(RecommendViewModel).GetHashCode().ToString()];

            Task.Run(() =>
            {
                foreach (RecommendFriendSummary AddedFriend in AddedFriends)
                {
                    var UserCollection = ConnectionHelper.DB.GetCollection<UserEntity>("UserEntity");
                    var FindUserQuery = Query<UserEntity>.EQ(u => u.Sid, AddedFriend.UserSID);
                    var FindedUser = UserCollection.FindOne(FindUserQuery);

                    if (null != FindedUser)
                    {
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new ThreadStart(() =>
                        {
                            this.Friends.Add(new Friend(FindedUser.Thumbnail, FindedUser.UserId, FindedUser.Sid, FindedUser.NickName, FindedUser.Comment));
                        }));
                    }
                }
            });
        }

        private void ExecuteNavigateToForwardCommand()
        {
            var RecommendViewUri = new Uri(FriendsViewNames.RecommendView, UriKind.Relative);
            this.m_RegionManager.RequestNavigate(FriendsRegionNames.NavigationRegion, RecommendViewUri);
        }
    }
}
