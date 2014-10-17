using BeautifulDB.Entities;
using BeautifulDB.Helpers;
using BeautifulTalkInfrastructure.AliveInformation;
using BeautifulTalkInfrastructure.DataModels;
using BeautifulTalkInfrastructure.Logger;
using BeautifulTalkInfrastructure.ProtocolFormat;
using Microsoft.Practices.Unity;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Rooms.Services
{
    public class DevRoomMsgListener : IRoomMsgListener
    {
        private const string MSG_SIDS = "msgsids";
        private const string MSG_SID = "msgsid";
        private const string NICKNAME = "nickname";
        private const string PHOTO_PATH = "photopath";
        private const string MSG_TYPE = "msgtype";
        private const string SENDER_ID = "senderid";
        private const string SENDER_SID = "sendersid";
        private const string CONTENT_TYPE = "contenttype";
        private const string ROOM_SID = "roomsid";
        private const string INTERESTS = "interests";
        private const string SEND_TIME = "sendtime";
        private const string MEMBER_SIDS = "membersids";
        private const string ACTIVE_MEMBER_SIDS = "activemembersids";
        public IRoomsTabHeaderInfoProvider RoomsView { get; set; }
        public IRoomMsgAnalyzable Analyzer { get; set; }

        public DevRoomMsgListener(IRoomMsgAnalyzable msgAnalyzer)
        {
            if (null == msgAnalyzer) throw new ArgumentNullException("msgAnalyzer");
            this.Analyzer = msgAnalyzer;
        }

        public void StartListen()
        {
            try
            {
                if (null != AuthRepository.MQKeyInfo)
                {
                    string strExchange = "beautifulexchange";
                    var factory = new ConnectionFactory() { HostName = BeautifulTalkProtocolSet.MQServerIP };
                    factory.UserName = "obk";
                    factory.Password = "dhqudrnjs";
                    factory.VirtualHost = "obkhost";
                    factory.Port = 5672;
                    factory.RequestedHeartbeat = 15;
                    factory.RequestedConnectionTimeout = 10000;

                    using (var connection = factory.CreateConnection())
                    {
                        connection.ConnectionShutdown += connection_ConnectionShutdown;

                        using (var channel = connection.CreateModel())
                        {
                            channel.ModelShutdown += channel_ModelShutdown;
                            var queueName = channel.QueueDeclare(AuthRepository.MQKeyInfo.UserSid, true, false, false, null);
                            channel.QueueBind(queueName, strExchange, AuthRepository.MQKeyInfo.UserSid + ".win.#");
                            var consumer = new QueueingBasicConsumer(channel);
                            channel.BasicConsume(queueName, false, consumer);

                            while (true)
                            {
                                BasicDeliverEventArgs ArrivedMessage = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                                if (null != ArrivedMessage)
                                {
                                    IDictionary<string, object> MQHeaders = ArrivedMessage.BasicProperties.Headers;
                                    string strContent = null;
                                    byte[] arBodyData = ArrivedMessage.Body;
                                    if (null != arBodyData) { strContent = Encoding.UTF8.GetString(arBodyData); }

                                    MsgType msgType             = (MsgType)(MQHeaders[MSG_TYPE]);
                                    string strSenderSid         = Encoding.UTF8.GetString((byte[])MQHeaders[SENDER_SID]);
                                    string strRoomSid           = Encoding.UTF8.GetString((byte[])MQHeaders[ROOM_SID]);

                                    switch (msgType)
                                    {
                                        case MsgType.ChatData:
                                            {
                                                ContentType contentType = (ContentType)MQHeaders[CONTENT_TYPE];
                                                string strMsgSid = Encoding.UTF8.GetString((byte[])MQHeaders[MSG_SID]);
                                                string strSenderId = Encoding.UTF8.GetString((byte[])MQHeaders[SENDER_ID]);
                                                string strNickName = Encoding.UTF8.GetString((byte[])MQHeaders[NICKNAME]);
                                                string strPhotoPath = Encoding.UTF8.GetString((byte[])MQHeaders[PHOTO_PATH]);
                                                long lSendTime = (long)MQHeaders[SEND_TIME];
                                                var bInterests = MQHeaders[INTERESTS] as List<object>;
                                                IList<string> arInterests = new List<string>(bInterests.Count);
                                                foreach (byte[] bInterest in bInterests) { arInterests.Add(Encoding.UTF8.GetString(bInterest)); }
                                                this.Analyzer.SaveUserIfNotExist(strSenderId, strSenderSid, strNickName, arInterests);

                                                string strMsgId = this.Analyzer.CreateRecordForChatData(strMsgSid, strSenderSid, strRoomSid, contentType, strContent, lSendTime);

                                                Task.Run(() =>
                                                {
                                                    var bMemberSids = MQHeaders[MEMBER_SIDS] as List<object>;
                                                    var bActiveMemberSids = MQHeaders[ACTIVE_MEMBER_SIDS] as List<object>;

                                                    IList<string> arMemberSids = new List<string>(bMemberSids.Count);
                                                    IList<string> arActiveMemberSids = new List<string>(bActiveMemberSids.Count);
                                                    
                                                    foreach (byte[] bMemberSid in bMemberSids) { arMemberSids.Add(Encoding.UTF8.GetString(bMemberSid)); }
                                                    foreach (byte[] bMemberSid in bActiveMemberSids) { arActiveMemberSids.Add(Encoding.UTF8.GetString(bMemberSid)); }

                                                    this.Analyzer.AnalyzeChatData(strMsgId, strMsgSid, strSenderSid, strRoomSid, strContent,
                                                        lSendTime, arMemberSids, arActiveMemberSids, contentType);
                                                });
                                                
                                                break;
                                            }
                                        case MsgType.Read:
                                            {
                                                var MessageCollection = ConnectionHelper.DB.GetCollection<MessageEntity>("MessageEntity");
                                                string strMsgSids = Encoding.UTF8.GetString((byte[])MQHeaders[MSG_SIDS]);
                                                string[] arMsgSids = strMsgSids.Split(',');

                                                foreach (string msgSid in arMsgSids)
                                                {
                                                    var FindMessageQuery = Query<MessageEntity>.EQ(m => m.Sid, msgSid);
                                                    var FindedMessage = MessageCollection.FindOne(FindMessageQuery);

                                                    if (null != FindedMessage)
                                                    {
                                                        IList<string> ReadMembers = FindedMessage.ReadMembers;
                                                        if (null == ReadMembers) { ReadMembers = new List<string>(); }
                                                        if (false == ReadMembers.Contains(strSenderSid)) { ReadMembers.Add(strSenderSid); }
                                                        var UpdateMessageQuery = Update<MessageEntity>
                                                            .Set(m => m.State, (int)MsgStatus.Read)
                                                            .Set(m => m.ReadMembers, ReadMembers);
                                                        MessageCollection.Update(FindMessageQuery, UpdateMessageQuery);
                                                    }
                                                }

                                                var RcvdReadMsg = new ReceivedReadMsg(strRoomSid, strMsgSids, strSenderSid);
                                                Task.Run(() => { this.Analyzer.AnalyzeRead(RcvdReadMsg); });
                                                break;
                                            }
                                        default: break;
                                    }

                                    channel.BasicAck(ArrivedMessage.DeliveryTag, false);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception AbstractException)
            {
                throw AbstractException;
            }
        }

        private void connection_ConnectionShutdown(IConnection connection, ShutdownEventArgs reason)
        {
            GlobalLogger.Log("connection_ConnectionShutdown");
            this.StartListen();
        }
        private void channel_ModelShutdown(IModel model, ShutdownEventArgs reason)
        {
            GlobalLogger.Log("channel_ModelShutdown");
            this.StartListen();
        }
    }
}
