using BeautifulDB.Entities;
using BeautifulDB.Helpers;
using BeautifulTalk.Modules.Chatting;
using BeautifulTalk.Modules.Chatting.ViewModels;
using BeautifulTalk.Modules.Chatting.Views;
using BeautifulTalk.Modules.Rooms.Models;
using BeautifulTalkInfrastructure.AliveInformation;
using BeautifulTalkInfrastructure.DataModels;
using BeautifulTalkInfrastructure.Interfaces;
using BeautifulTalkInfrastructure.ProtocolFormat;
using BeautifulTalkInfrastructure.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace BeautifulTalk.Modules.Rooms.Controllers
{
    public class RoomsController : IRoomsControlable
    {
        private SubscriptionToken m_FriendDoubleClickedEvtToken;
        private readonly IRoomsDictionaryBehavior<string, ChattingShellView> m_ChattingRoomsDictionary;
        private RoomCollection m_Rooms;
        private IUnityContainer m_Container;
        private IEventAggregator m_EventAggregator;
        private ITabHeaderNotificationProvider<Int32> m_TabHeaderNotification;

        public RoomsController(IUnityContainer container, IEventAggregator eventAggregator,
            RoomCollection rooms)
        {
            if (null == container) throw new ArgumentNullException("container");
            if (null == eventAggregator) throw new ArgumentNullException("eventAggregator");
            if (null == rooms) throw new ArgumentNullException("rooms");
            
            this.m_Rooms = rooms;
            this.m_Container = container;
            this.m_EventAggregator = eventAggregator;
            this.m_ChattingRoomsDictionary = new RoomsDictionary();
            this.SubscribeEvents();
        }
        /*
        public RoomsController(IUnityContainer container, IEventAggregator eventAggregator,
            RoomCollection rooms, ITabHeaderNotificationProvider<Int32> tabHeaderNotification)
        {
            if (null == container) throw new ArgumentNullException("container");
            if (null == eventAggregator) throw new ArgumentNullException("eventAggregator");
            if (null == rooms) throw new ArgumentNullException("rooms");
            if (null == tabHeaderNotification) throw new ArgumentNullException("tabHeaderNotification");

            this.m_Rooms = rooms;
            this.m_Container = container;
            this.m_EventAggregator = eventAggregator;
            this.m_TabHeaderNotification = tabHeaderNotification;
            this.m_ChattingRoomsDictionary = new RoomsDictionary();
            this.SubscribeEvents();
        }
        */
        private void SubscribeEvents()
        {
            var FriendDoubleClickedEvt = this.m_EventAggregator.GetEvent<FriendDoubleClickedEvent>();

            if (null != this.m_FriendDoubleClickedEvtToken)
            {
                FriendDoubleClickedEvt.Unsubscribe(this.m_FriendDoubleClickedEvtToken);
            }

            this.m_FriendDoubleClickedEvtToken = FriendDoubleClickedEvt.Subscribe(FriendDoubleClicked, ThreadOption.BackgroundThread, true, null);
        }

        private void FriendDoubleClicked(FriendDoubleClickParams parameters)
        {
            this.AddRoom(parameters.Sid, parameters.Members, parameters.UnReadMsgCount, parameters.LastMsgSummary, parameters.LastMsgDate, parameters.ThumbnailPath);
            this.CreateChattingShellOrActivate(parameters.Sid);
        }
        public void CreateChattingShellOrActivate(string strRoomSID)
        {
            if (false == this.m_ChattingRoomsDictionary.DoesExist(strRoomSID))
            {
                this.CreateChattingShell(strRoomSID);
            }
            else
            {
                this.ActivateChattingShell(strRoomSID);
            }
        }

        private void CreateChattingShell(string strRoomSID)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new ThreadStart(() =>
            {
                var ChattingViewModel = new ChattingViewModel(strRoomSID, this);
                var ChattingView = new ChattingView(ChattingViewModel);

                var ChattingShellViewModel = new ChattingShellViewModel(strRoomSID, this);
                var ChattingShellView = new ChattingShellView(ChattingShellViewModel);
                ChattingShellView.ChattingViewModel = ChattingViewModel;
                ChattingShellView.Content = ChattingView;
                ChattingViewModel.ShellView = ChattingShellView;

                if (true == this.m_ChattingRoomsDictionary.Register(strRoomSID, ChattingShellView))
                {
                    ChattingShellView.ShowChattingShellView();
                }
            }));
        }

        private void ActivateChattingShell(string strRoomSID)
        {
            ChattingShellView FindedChattingWindow = this.m_ChattingRoomsDictionary.GetValue(strRoomSID);

            if (null != FindedChattingWindow)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new ThreadStart(() =>
                {
                    FindedChattingWindow.Activate();
                }));
            }
        }
        public void RemoveChattingShell(string strRoomSID)
        {
            this.m_ChattingRoomsDictionary.Remove(strRoomSID);
        }

        public void AddRoom(string strSid, IList<string> arMembers, int nUnReadMsgCount, string strLastMsgSummary, long lLastMsgDate, string strThumbnailPath)
        {
            var FindedRoom = this.m_Rooms.SingleOrDefault(r => r.Sid == strSid);
            
            if (null == FindedRoom) 
            {
                var UserCollection = ConnectionHelper.DB.GetCollection<UserEntity>("UserEntity");
                IList<string> MemberNickNames = new List<string>();

                foreach (string strUserSid in arMembers)
                {
                    if (strUserSid == AuthRepository.MQKeyInfo.UserSid) continue;
                    var FindUserQuery = Query<UserEntity>.EQ(u => u.Sid, strUserSid);
                    var FindedUser = UserCollection.FindOne(FindUserQuery);
                    if (null != FindedUser) { MemberNickNames.Add(FindedUser.NickName); }
                }

                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Send, new ThreadStart(() =>
                {
                    this.m_Rooms.Add(new Room(strSid, MemberNickNames, nUnReadMsgCount, strLastMsgSummary, lLastMsgDate, strThumbnailPath));
                    //this.m_TabHeaderNotification.HeaderNotification += nUnReadMsgCount;
                }));
            }
        }

        public bool ExistsRoom(string strRoomSID)
        {
            var FindedRoom = this.m_Rooms.FirstOrDefault(r => r.Sid == strRoomSID);

            if (null == FindedRoom) return false;
            return true;
        }
        public void UpdateRoom(string strMsgId, string strSenderSid, string strMsgSid, IList<string> arActiveMemberSids, string strSid, int nUnReadMsgCount, 
            ContentType contentType, string strContent, long lLastMsgDate, string strThumbnailPath)
        {
            var FindedRoom = this.m_Rooms.FirstOrDefault(r => r.Sid == strSid);

            if (null != FindedRoom)
            {
                var RoomCollection = ConnectionHelper.DB.GetCollection<RoomEntity>("RoomEntity");
                var FindRoomQuery = Query<RoomEntity>.EQ(r => r.Sid, strSid);

                RoomEntity FindedRoomEntity = RoomCollection.FindOne(FindRoomQuery);
                
                if (null != FindedRoomEntity)
                {
                    int nNewUnReadMsgCount = (FindedRoom.UnReadMsgCount + nUnReadMsgCount);

                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Send, new ThreadStart(() =>
                    {
                        FindedRoom.UnReadMsgCount = nNewUnReadMsgCount;
                        FindedRoom.LastMsgSummary = strContent;
                        FindedRoom.LastMsgDate = lLastMsgDate;
                        FindedRoom.ThumbnailPath = strThumbnailPath;
                        //this.m_TabHeaderNotification.HeaderNotification += nUnReadMsgCount;
                    }));

                    var UpdateRoomQuery = Update<RoomEntity>
                                        .Set(r => r.ActiveMemberSids, arActiveMemberSids)
                                        .Set(r => r.UnReadMsgCount, nNewUnReadMsgCount)
                                        .Set(r => r.LastMsgSummary, strContent)
                                        .Set(r => r.LastMsgDate, lLastMsgDate)
                                        .Set(r => r.ThumbnailPath, strThumbnailPath);

                    RoomCollection.Update(FindRoomQuery, UpdateRoomQuery);

                    if (true == this.m_ChattingRoomsDictionary.DoesExist(strSid))
                    {
                        var FindedChattingRoom = this.m_ChattingRoomsDictionary.GetValue(strSid);

                        if (null != FindedChattingRoom)
                        {
                            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Send, new ThreadStart(() =>
                            {
                                bool bIsActivatedView = FindedChattingRoom.IsActiveChattingShellView();
                                ReceivedMsg rcvMsg = new ReceivedMsg(strMsgId, strSenderSid, strSid, strMsgSid, contentType, strContent,
                                    lLastMsgDate, 0, strThumbnailPath, bIsActivatedView);
                                FindedChattingRoom.ChattingViewModel.ReceiveChatMsgCommand.Execute(rcvMsg);

                                if (true == bIsActivatedView)
                                {
                                    this.ResetUnReadCountForRoom(strSid);
                                }
                            }));
                        }
                    }
                }
            }
        }

        public void ResetUnReadCountForRoom(string strRoomSID)
        {
            var RoomCollection = ConnectionHelper.DB.GetCollection<RoomEntity>("RoomEntity");
            var FindRoomQuery = Query<RoomEntity>.EQ(r => r.Sid, strRoomSID);
            var UpdateRoomQuery = Update<RoomEntity>.Set(r => r.UnReadMsgCount, 0);

            RoomEntity FindedRoomEntity = RoomCollection.FindOne(FindRoomQuery);
            var FindedRoom = this.m_Rooms.SingleOrDefault(r => r.Sid == strRoomSID);

            if (null != FindedRoom && null != FindedRoomEntity)
            {
                FindedRoom.UnReadMsgCount = 0;
                //this.m_TabHeaderNotification.HeaderNotification -= FindedRoomEntity.UnReadMsgCount;
                RoomCollection.Update(FindRoomQuery, UpdateRoomQuery);
            }
        }


        public void UpdateLastMsg(string strRoomSID, string strContent, long lSendTime)
        {
            var RoomCollection = ConnectionHelper.DB.GetCollection<RoomEntity>("RoomEntity");
            var FindRoomQuery = Query<RoomEntity>.EQ(r => r.Sid, strRoomSID);
            var UpdateRoomQuery = Update<RoomEntity>
                .Set(r => r.LastMsgSummary, strContent)
                .Set(r => r.LastMsgDate, lSendTime);

            RoomEntity FindedRoomEntity = RoomCollection.FindOne(FindRoomQuery);
            var FindedRoom = this.m_Rooms.SingleOrDefault(r => r.Sid == strRoomSID);

            if (null != FindedRoom && null != FindedRoomEntity)
            {
                FindedRoom.LastMsgSummary = strContent;
                FindedRoom.LastMsgDate = lSendTime;
                RoomCollection.Update(FindRoomQuery, UpdateRoomQuery);
            }
        }

        public void ReadMessagesForRoom(ReceivedReadMsg rcvdReadMsg)
        {
            if (true == this.m_ChattingRoomsDictionary.DoesExist(rcvdReadMsg.RoomSid))
            {
                var FindedChattingShellView = this.m_ChattingRoomsDictionary.GetValue(rcvdReadMsg.RoomSid);
                FindedChattingShellView.ChattingViewModel.ReceiveReadMsgCommand.Execute(rcvdReadMsg);
            }
        }
    }
}
