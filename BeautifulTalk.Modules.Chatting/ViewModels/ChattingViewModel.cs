using BeautifulDB.Entities;
using BeautifulDB.Helpers;
using BeautifulTalk.Modules.Chatting.Models;
using BeautifulTalk.Modules.Chatting.Services;
using BeautifulTalk.Modules.Chatting.Services.Mocks;
using BeautifulTalk.Modules.Chatting.Views;
using BeautifulTalkInfrastructure.AliveInformation;
using BeautifulTalkInfrastructure.DataModels;
using BeautifulTalkInfrastructure.Generators;
using BeautifulTalkInfrastructure.Interfaces;
using BeautifulTalkInfrastructure.Logger;
using BeautifulTalkInfrastructure.ProtocolFormat;
using BeautifulTalkInfrastructure.PubSubEvents;
using CommonUtility;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace BeautifulTalk.Modules.Chatting.ViewModels
{
    public class ChattingViewModel : BindableBase, IChattingViewModel
    {
        private IDictionary<string, Brush> m_AnonymousThumbnailDictionary;
        private IList<UnReadMsg> m_UnReadMessages;
        private IChattingViewBehavior m_ChattingView;
        private IReadMessageService m_ReadMsgService;
        private ILoadMessagesService m_LoadMsgService;
        private ISendMessageService m_SendMessageService;
        private IRoomsControlable m_RoomsController;
        private DelegateCommand<ReceivedMsg> m_ReceiveChatMsgCommand;
        private DelegateCommand<ReceivedReadMsg> m_ReceiveReadMsgCommand;
        public string RoomSID { get; private set; }
        public IActiveChattingShellView ShellView { get; set; }
        public DelegateCommand<IChattingViewBehavior> InitialLoadedCommand { get; private set; }
        public DelegateCommand<UIElement> InitialFocusCommand { get; private set; }
        public DelegateCommand InputTextChangedCommand { get; private set; }
        public DelegateCommand<string> SendTextCommand { get; private set; }
        public DelegateCommand<CommunicationMsg> ReSendCommand { get; private set; }
        public DelegateCommand<CommunicationMsg> DeletePendingMsgCommand { get; private set; }
        public DelegateCommand<ScrollViewer> DefineScrollingCommand { get; private set; }
        public DelegateCommand<ReceivedMsg> ReceiveChatMsgCommand { get { return this.m_ReceiveChatMsgCommand; } }
        public DelegateCommand<ReceivedReadMsg> ReceiveReadMsgCommand { get { return this.m_ReceiveReadMsgCommand; } }
        public DelegateCommand AttachFileCommand { get; private set; }
        public MsgCollection Messages { get; private set; }
        public ChattingViewModel(string strRoomSID, IRoomsControlable roomsController)
        {
            if (null == strRoomSID) throw new ArgumentNullException("strRoomSID");
            if (null == roomsController) throw new ArgumentNullException("roomsController");

            this.m_AnonymousThumbnailDictionary = new Dictionary<string, Brush>();
            this.m_AnonymousThumbnailDictionary.Add(AuthRepository.MQKeyInfo.UserSid, ColorGenerator.Instance.GetRandomBrush());
            this.RoomSID = strRoomSID;
            this.m_RoomsController = roomsController;
            
            this.m_SendMessageService = new SendMessageService();
            this.m_LoadMsgService = new LoadMessagesService(AuthRepository.MQKeyInfo.UserSid, this.m_AnonymousThumbnailDictionary);
            this.m_ReadMsgService = new ReadMessageService();

            this.InitialLoadedCommand = new DelegateCommand<IChattingViewBehavior>(ExecuteInitialLoadedCommand);
            this.InitialFocusCommand = new DelegateCommand<UIElement>(ExecuteInitialFocusCommand);
            this.InputTextChangedCommand = new DelegateCommand(ExecuteInputTextChangedCommand);
            this.ReSendCommand = new DelegateCommand<CommunicationMsg>(ExecuteReSendCommand);
            this.DeletePendingMsgCommand = new DelegateCommand<CommunicationMsg>(ExecuteDeletePendingMsgCommand);
            this.SendTextCommand = new DelegateCommand<string>(ExecuteSendTextCommand, CanExecuteSendTextCommand);
            this.DefineScrollingCommand = new DelegateCommand<ScrollViewer>(ExecuteDefineScrollingCommand);
            this.AttachFileCommand = new DelegateCommand(ExecuteAttachFileCommand);
            this.m_ReceiveChatMsgCommand = new DelegateCommand<ReceivedMsg>(ExecuteReceiveChatMsgCommand);
            this.m_ReceiveReadMsgCommand = new DelegateCommand<ReceivedReadMsg>(ExecuteReceiveReadMsgCommand);
            this.Messages = new MsgCollection(this.m_LoadMsgService.LoadMessages(strRoomSID, 0).Reverse());
            this.m_UnReadMessages = this.m_LoadMsgService.LoadUnReadMessages(strRoomSID);
        }

        private void ExecuteReceiveReadMsgCommand(ReceivedReadMsg rcvdReadMsg)
        {
            var ChatMsgs = this.Messages.OfType<ChatMsg>();
            string[] arMsgSids = rcvdReadMsg.MsgSids.Split(',');

            foreach (string strMsgSid in arMsgSids)
            {
                try
                {
                    var TargetMessage = ChatMsgs.Single(m => m.Sid == strMsgSid);
                    TargetMessage.ReadMembersCount += 1;
                }
                catch (InvalidOperationException invalidOperException)
                {
                    GlobalLogger.Log("Target message doesn't exist.\n" + invalidOperException.Message );
                }
            }
        }
        private void ExecuteAttachFileCommand()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.png;*.jpeg;*.jpg;*.gif)|*.png;*.jpeg;*.jpg;*.gif";

            if (true == ofd.ShowDialog())
            {
                byte[] imageArray = File.ReadAllBytes(ofd.FileName);
            }
        }

        private void ExecuteInitialLoadedCommand(IChattingViewBehavior chattingView)
        {
            if (null == chattingView) throw new ArgumentNullException("chattingView");
            this.m_ChattingView = chattingView;

            if (null != Messages)
            {
                if (0 < Messages.Count)
                {
                    this.m_ChattingView.ScrollIntoView(Messages.ElementAt(Messages.Count - 1));
                }
            }
        }

        private void ExecuteInitialFocusCommand(UIElement targetElement)
        {
            targetElement.Focus();
        }

        private void ExecuteInputTextChangedCommand()
        {
            this.SendTextCommand.RaiseCanExecuteChanged();
        }

        #region SendCommands
        private void ExecuteSendTextCommand(string strText)
        {
            var MessageCollection = ConnectionHelper.DB.GetCollection<MessageEntity>("MessageEntity");
            var NewMessage = new MessageEntity(this.RoomSID, strText, (int)MsgStatus.Sending, (int)ContentType.Text, AuthRepository.MQKeyInfo.UserSid, 0, null);
            MessageCollection.Save(NewMessage);

            var SendMessage = new ChatMsg(NewMessage.Id.ToString(), null, this.RoomSID, strText, ContentType.Text, 0, MsgStatus.Sending, 0, 
                AuthRepository.MQKeyInfo.NickName, AuthRepository.MQKeyInfo.ThumbnailPath, this.m_AnonymousThumbnailDictionary[AuthRepository.MQKeyInfo.UserSid]);
            
            this.Messages.Add(SendMessage);
            this.EndSendTextCommand(SendMessage);

            Task.Run(() =>
            {
                var RoomCollection = ConnectionHelper.DB.GetCollection<RoomEntity>("RoomEntity");
                var FindRoomQuery = Query<RoomEntity>.EQ(r => r.Sid, this.RoomSID);
                var FindedRoom = RoomCollection.FindOne(FindRoomQuery);

                if (null != FindedRoom)
                {
                    var ActiveMemberSids = FindedRoom.ActiveMemberSids;
                    ActiveMemberSids.Remove(AuthRepository.MQKeyInfo.UserSid);
                    SendingMsg WillSendMsg = new SendingMsg(ActiveMemberSids, this.RoomSID, (int)ContentType.Text,
                        strText, AuthRepository.MQKeyInfo.UserSid, SendMessage);

                    var Result = this.m_SendMessageService.SendMessage(WillSendMsg);
                    this.UpdateSendMessage(Result, SendMessage, MessageCollection, strText, NewMessage.Id.ToString());
                }
            });
        }

        private void UpdateSendMessage(SendingMsgResult result, ICommunicationMsgInfo communicationMsg, MongoCollection<MessageEntity> msgCollection, 
            string strContent, string strSendMsgId)
        {
            var FindMsgQuery = Query<MessageEntity>.EQ(m => m.Id, new ObjectId(strSendMsgId));

            if (200 == result.Status)
            {
                CommunicationMsg SentMsg = communicationMsg as CommunicationMsg;

                try
                {
                    var RecentSuccessMsg = this.Messages.OfType<CommunicationMsg>().Last(m => 
                        m.MsgStatus != MsgStatus.Failed && m.MsgStatus != MsgStatus.Sending);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.Messages.Remove(SentMsg);

                        int nIndexOfRecentSuccessdMsg = this.Messages.IndexOf(RecentSuccessMsg);
                        this.Messages.Insert(nIndexOfRecentSuccessdMsg + 1, SentMsg);
                    });
                }
                catch (InvalidOperationException)
                {
                    //If you cannot find RecentSuccessMsg
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.Messages.Remove(SentMsg);
                        this.Messages.Insert(0, SentMsg);
                    });
                }

                communicationMsg.MsgStatus = MsgStatus.Sent;
                communicationMsg.SendTime = result.SentTime;

                var UpdateMsgQuery = Update<MessageEntity>
                    .Set(m => m.State, (int)MsgStatus.Sent)
                    .Set(m => m.SendTime, result.SentTime)
                    .Set(m => m.Sid, result.MsgSid);

                msgCollection.Update(FindMsgQuery, UpdateMsgQuery);
                this.m_RoomsController.UpdateLastMsg(this.RoomSID, strContent, result.SentTime);
            }
            else
            {
                communicationMsg.MsgStatus = MsgStatus.Failed;
                var UpdateMsgQuery = Update<MessageEntity>.Set(m => m.State, (int)MsgStatus.Failed);

                msgCollection.Update(FindMsgQuery, UpdateMsgQuery);
            }
        }

        private void EndSendTextCommand(object objMsg)
        {
            this.m_ChattingView.ScrollIntoView(objMsg);
            this.m_ChattingView.ClearInput();
        }

        private bool CanExecuteSendTextCommand(string strText)
        {
            if (true == string.IsNullOrEmpty(strText))
            {
                return false;
            }

            return true;
        }
        #endregion SendCommands

        #region Receive Commands
        private void ExecuteReceiveChatMsgCommand(ReceivedMsg rcvMsg)
        {
            var MessageCollection = ConnectionHelper.DB.GetCollection<MessageEntity>("MessageEntity");
            var FindMessageQuery = Query<MessageEntity>.EQ(m => m.Id, new ObjectId(rcvMsg.Id));
            var FindedMessage = MessageCollection.FindOne(FindMessageQuery);

            var UserCollection = ConnectionHelper.DB.GetCollection<UserEntity>("UserEntity");
            var FindUserQuery = Query<UserEntity>.EQ(u => u.Sid, rcvMsg.SenderSid);
            var FindedUser = UserCollection.FindOne(FindUserQuery);

            if (null != FindedUser && null != FindedMessage)
            {
                string strFindedUserSid = FindedUser.Sid;

                if (string.IsNullOrEmpty(rcvMsg.ThumbnailPath)) 
                {
                    if (false == this.m_AnonymousThumbnailDictionary.ContainsKey(strFindedUserSid))
                    {
                        this.m_AnonymousThumbnailDictionary.Add(strFindedUserSid, ColorGenerator.Instance.GetRandomBrush());
                    }
                }

                var RcvMessage = new OpponentMsg(rcvMsg.Id, rcvMsg.Sid, rcvMsg.RoomSid, rcvMsg.Content, rcvMsg.ContentType,
                    rcvMsg.SendTime, MsgStatus.Received, rcvMsg.ReadMembersCount, FindedUser.NickName, rcvMsg.ThumbnailPath,
                    this.m_AnonymousThumbnailDictionary[strFindedUserSid]);

                this.Messages.Add(RcvMessage);
                this.EndReceiveMsgCommand(RcvMessage);

                var WillAddUnReadMsg = new UnReadMsg(FindedMessage.Id, FindedMessage.ReadMembers, rcvMsg.Sid, rcvMsg.RoomSid, AuthRepository.MQKeyInfo.UserSid);
                this.m_UnReadMessages.Add(WillAddUnReadMsg);

                if (true == rcvMsg.IsActivatedView)
                {
                    this.RequestReadMsgs();
                }
            }
        }
        private void EndReceiveMsgCommand(object objMsg)
        {
            this.m_ChattingView.ScrollIntoView(objMsg);
        }
        #endregion Receive Commands

        #region Resend Commands
        private void ExecuteReSendCommand(CommunicationMsg resendMsg)
        {
            Task.Run(() =>
            {
                var MessageCollection = ConnectionHelper.DB.GetCollection<MessageEntity>("MessageEntity");
                var RoomCollection = ConnectionHelper.DB.GetCollection<RoomEntity>("RoomEntity");
                var FindRoomQuery = Query<RoomEntity>.EQ(r => r.Sid, this.RoomSID);
                var FindedRoom = RoomCollection.FindOne(FindRoomQuery);

                if (null != FindedRoom)
                {
                    resendMsg.MsgStatus = MsgStatus.Sending;
                    var ActiveMemberSids = FindedRoom.ActiveMemberSids;
                    ActiveMemberSids.Remove(AuthRepository.MQKeyInfo.UserSid);
                    SendingMsg WillSendMsg = new SendingMsg(ActiveMemberSids, resendMsg.RoomSid, (int)resendMsg.ContentType,
                        resendMsg.Content, AuthRepository.MQKeyInfo.UserSid, resendMsg);

                    var Result = this.m_SendMessageService.SendMessage(WillSendMsg);
                    this.UpdateSendMessage(Result, resendMsg, MessageCollection, resendMsg.Content, resendMsg.Id.ToString());
                }
            });
        }
        #endregion Resend Commands

        private void ExecuteDeletePendingMsgCommand(CommunicationMsg deleteMsg)
        { 
            var MessageCollection = ConnectionHelper.DB.GetCollection<MessageEntity>("MessageEntity");
            var FindMessageQuery = Query<MessageEntity>.EQ(m => m.Id, new ObjectId(deleteMsg.Id));
            MessageCollection.Remove(FindMessageQuery);

            this.Messages.Remove(deleteMsg);
        }
        public void RequestReadMsgs()
        {
            bool bIsSuccess = this.m_ReadMsgService.ReadMessages(this.m_UnReadMessages);
            
            if (true == bIsSuccess)
            {
                var MessageCollection = ConnectionHelper.DB.GetCollection<MessageEntity>("MessageEntity");
                var ChatMsgs = this.Messages.OfType<ChatMsg>();

                foreach (UnReadMsg msg in this.m_UnReadMessages)
                {
                    IList<string> ReadMembers = msg.ReadMembers;
                    if (null == ReadMembers) { ReadMembers = new List<String>(); }
                    if (false == ReadMembers.Contains(AuthRepository.MQKeyInfo.UserSid)) { ReadMembers.Add(AuthRepository.MQKeyInfo.UserSid); }
                    var UpdateMessageQuery = Update<MessageEntity>
                        .Set(m => m.State, (int)MsgStatus.Read)
                        .Set(m => m.ReadMembers, ReadMembers);

                    var FindMessageQuery = Query<MessageEntity>.EQ(m => m.Id, msg.Id);
                    MessageCollection.Update(FindMessageQuery, UpdateMessageQuery);

                    try
                    {
                        var TargetMessage = ChatMsgs.Single(m => m.Sid == msg.Sid);
                        TargetMessage.ReadMembersCount += 1;
                    }
                    catch (InvalidOperationException invalidOperException)
                    {
                        GlobalLogger.Log(invalidOperException.Message);
                    }
                }
                
                this.m_UnReadMessages.Clear();
            }
        }
        private void ExecuteDefineScrollingCommand(ScrollViewer scrollViewer)
        {
            scrollViewer.ScrollChanged -= ScrollViewer_ScrollChanged;
            scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (0 == e.VerticalOffset && 0 > e.VerticalChange)
            {
                try
                {
                    var TopMessage = this.Messages.First(m => m is ChatMsg);

                    if (null != TopMessage)
                    {
                        var TopMsg = (TopMessage as CommunicationMsg);
                        if (MsgStatus.Failed != TopMsg.MsgStatus)
                        {
                            var LoadedMsgs = this.m_LoadMsgService.LoadMessages(this.RoomSID, TopMsg.SendTime);

                            foreach (var Msg in LoadedMsgs)
                            {
                                this.Messages.Insert(0, Msg);
                            }
                        }
                    }

                    this.m_ChattingView.ScrollIntoView(TopMessage);
                }
                catch (NullReferenceException nullRefException)
                {
                    GlobalLogger.Log(nullRefException.Message);
                }
                catch (Exception unExpectedException)
                {
                    GlobalLogger.Log(unExpectedException.Message);
                }
            }
        }
    }
}
