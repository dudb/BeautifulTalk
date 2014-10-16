using BeautifulTalk.Modules.Recommend.Models;
using BeautifulTalk.Modules.Recommend.Services;
using BeautifulTalkInfrastructure.AliveInformation;
using BeautifulTalkInfrastructure.Interfaces;
using BeautifulTalkInfrastructure.ProtocolFormat;
using BeautifulTalkInfrastructure.PubSubEvents;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BeautifulTalk.Modules.Recommend.ViewModels
{
    public class RecommendViewModel : BindableBase, IRecommendTabHeaderInfoProvider
    {
        private DependencyObject m_TabHeaderImage;
        private ILoggerFacade m_Logger;
        private IUnityContainer m_UnityContainer;
        private IEventAggregator m_EventAggregator;
        private ICollectRecommendUsers m_CollectRecommendUsers;
        private FriendSummaryCollection m_RecommendFriends;
        public FriendSummaryCollection RecommendFriends 
        {
            get { return this.m_RecommendFriends; }
            set { SetProperty(ref this.m_RecommendFriends, value); }
        }
        public DelegateCommand<string> AddFriendCommand { get; private set; }
        public DependencyObject HeaderContent { get { return m_TabHeaderImage; } }
        public int HeaderNotification { get { return 0; } }
        public MQKeySet MQKeyInfo { get; private set; }
        public RecommendViewModel(ILoggerFacade logger, IUnityContainer unityContainer, IEventAggregator eventAggregator,
            ICollectRecommendUsers collectRecommendUsers)
        {
            if (null == logger) throw new ArgumentNullException("logger");
            if (null == unityContainer) throw new ArgumentNullException("unityContainer");
            if (null == eventAggregator) throw new ArgumentNullException("eventAggregator");
            if (null == collectRecommendUsers) throw new ArgumentNullException("collectRecommendUsers");

            this.m_Logger = logger;
            this.m_UnityContainer = unityContainer;
            this.m_EventAggregator = eventAggregator;
            this.m_CollectRecommendUsers = collectRecommendUsers;

            this.AddFriendCommand = new DelegateCommand<string>(ExecuteAddFriendCommand);
            InitializeHeaderImages();
            eventAggregator.GetEvent<GiveMQKeySetEvent>().Publish(IntializeRecommendContents);
        }

        private void ExecuteAddFriendCommand(string strUserID)
        {
            using (var httpClient = new HttpClient())
            {
                dynamic parameter = new JObject();
                parameter.usersid = MQKeyInfo.UserSid;
                parameter.friendid = strUserID;
                var JsonParameters = JsonConvert.SerializeObject(parameter);
                var postdataString = new StringContent(JsonParameters, new UTF8Encoding(), "application/json");

                var responseMessage = httpClient.PostAsync(
                    string.Format("{0}{1}", BeautifulTalkProtocolSet.ServerURIwithPort, BeautifulTalkProtocolSet.AddFriendURI),
                    postdataString).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var FindedFriend = this.RecommendFriends.FirstOrDefault(u => u.UserID == strUserID);
                    this.RecommendFriends.Remove(FindedFriend);
                }
            }
        }
        private void IntializeRecommendContents(MQKeySet mqKeyInfo)
        {
            this.SetMQKeyInfo(mqKeyInfo);
            this.UpdateRecommendUsers();
        }
        private void SetMQKeyInfo(MQKeySet mqKeyInfo)
        {
            this.MQKeyInfo = mqKeyInfo;
        }

        private void UpdateRecommendUsers()
        {
            this.RecommendFriends= this.m_CollectRecommendUsers.Collect(20, this.MQKeyInfo.Account);
        }
        private void InitializeHeaderImages()
        {
            try
            {
                Image TabHeaderImage = new Image();
                TabHeaderImage.Source = new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Recommend;component/Resources/Images/base_recommend.png", UriKind.Relative));
                m_TabHeaderImage = TabHeaderImage;
            }
            catch (UriFormatException uriFormatException)
            {
                m_Logger.Log("Invalid Uri Format for ImageSource inside RecommendModule\n" + uriFormatException.Message, Category.Exception, Priority.Medium);
                throw uriFormatException;
            }
            catch (ArgumentNullException argsNullException)
            {
                m_Logger.Log("UriString is Null inside RecommendModule\n" + argsNullException.Message, Category.Exception, Priority.Medium);
                throw argsNullException;
            }
            catch (ArgumentException argsException)
            {
                m_Logger.Log("UriKind is invalid inside RecommendModule\n" + argsException.Message, Category.Exception, Priority.Medium);
                throw argsException;
            }
        }
    }
}
