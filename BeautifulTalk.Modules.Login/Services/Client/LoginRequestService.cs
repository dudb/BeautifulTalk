using BeautifulDB.Entities;
using BeautifulDB.Helpers;
using BeautifulTalk.Modules.Login.Makers;
using BeautifulTalk.Modules.Login.Models;
using BeautifulTalkInfrastructure.AliveInformation;
using BeautifulTalkInfrastructure.Generators;
using BeautifulTalkInfrastructure.Logger;
using BeautifulTalkInfrastructure.ProtocolFormat;
using BeautifulTalkInfrastructure.PubSubEvents;
using CommonControl.BusyIndicator;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace BeautifulTalk.Modules.Login.Services.Client
{
    public class LoginRequestService : ILoginTriableService
    {
        private ILoggerFacade m_Logger;
        private IUnityContainer m_Container;
        private IEventAggregator m_EventAggregator;
        private IRequestAccessInfoService m_RequestAccessInfoService;
        private IRequestMQKeysService m_RequestMQKeysService;
        public LoginRequestService(ILoggerFacade logger, IUnityContainer container, IEventAggregator eventAggregator, 
            IRequestAccessInfoService requestAccessInfoService, IRequestMQKeysService requestMQKeysService)
        {
            if (null == logger) throw new ArgumentNullException("logger");
            if (null == container) throw new ArgumentNullException("container");
            if (null == eventAggregator) throw new ArgumentNullException("eventAggregator");
            if (null == requestAccessInfoService) throw new ArgumentNullException("requestAccessInfoService");
            if (null == requestMQKeysService) throw new ArgumentNullException("requestMQKeysService");

            this.m_Logger = logger;
            this.m_Container = container;
            this.m_EventAggregator = eventAggregator;
            this.m_RequestAccessInfoService = requestAccessInfoService;
            this.m_RequestMQKeysService = requestMQKeysService;
        }

        public bool TryLogin(DelegateCommand<UIElement> loginCommand, SmoothBusyIndicator busyIndicator, UIElement loginButton, LoginModel loginModel)
        {
            bool bIsSuccessAuth = false;
            this.m_Logger.Log("TryLogin Raised", Category.Info, Priority.None);

            try
            {
                Application.Current.Dispatcher.Invoke(() => { loginButton.IsEnabled = false; });
                busyIndicator.IsBusy = true;
                
                AuthRepository.AccessInfo = this.m_RequestAccessInfoService.RequestAccessInfo(loginModel.Id, loginModel.Password);

                if (null != AuthRepository.AccessInfo)
                {
                    AuthRepository.MQKeyInfo = this.m_RequestMQKeysService.RequestMQKeys(AuthRepository.AccessInfo.TokenType, AuthRepository.AccessInfo.AccessToken);

                    if (null != AuthRepository.MQKeyInfo)
                    {
                        var FindMeQuery = Query<UserEntity>.EQ(u => u.Sid, AuthRepository.MQKeyInfo.UserSid);
                        var UserCollection = ConnectionHelper.DB.GetCollection<UserEntity>("UserEntity");
                        var FindedUser = UserCollection.FindOne(FindMeQuery);

                        if (null == FindedUser)
                        {
                            var Me = new UserEntity(AuthRepository.MQKeyInfo.Account, AuthRepository.MQKeyInfo.UserSid, AuthRepository.MQKeyInfo.NickName,
                                AuthRepository.MQKeyInfo.Interests);
                            UserCollection.Save(Me);
                        }

                        var FindFriendQuery = Query<FriendsEntity>.EQ(u => u.UserSID, AuthRepository.MQKeyInfo.UserSid);
                        var FriendCollection = ConnectionHelper.DB.GetCollection<FriendsEntity>("FriendsEntity");
                        var FindedFriend = FriendCollection.FindOne(FindFriendQuery);
                        if (null == FindedFriend) { FriendCollection.Save(new FriendsEntity(AuthRepository.MQKeyInfo.UserSid, new List<string>())); }

                        bIsSuccessAuth = true;
                    }
                }
            }
            catch (ArgumentNullException ParameterNullException)
            {
                this.m_Logger.Log(ParameterNullException.Message, Category.Exception, Priority.None);
            }
            catch (InvalidCastException InvalidCastAboutRequestParameters)
            {
                this.m_Logger.Log(InvalidCastAboutRequestParameters.Message, Category.Exception, Priority.None);
            }
            catch (Exception UnExpectedException)
            {
                this.m_Logger.Log(UnExpectedException.Message, Category.Exception, Priority.None);
            }
            finally
            {
                busyIndicator.IsBusy = false;
                Application.Current.Dispatcher.Invoke(() => { loginButton.IsEnabled = true; });
            }

            return bIsSuccessAuth;
        }
    }
}
