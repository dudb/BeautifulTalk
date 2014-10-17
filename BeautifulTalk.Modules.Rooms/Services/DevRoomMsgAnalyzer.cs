using BeautifulDB.Entities;
using BeautifulDB.Helpers;
using BeautifulTalkInfrastructure.AliveInformation;
using BeautifulTalkInfrastructure.DataModels;
using BeautifulTalkInfrastructure.Interfaces;
using BeautifulTalkInfrastructure.Logger;
using BeautifulTalkInfrastructure.ProtocolFormat;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BeautifulTalk.Modules.Rooms.Services
{
    public class DevRoomMsgAnalyzer : IRoomMsgAnalyzable
    {
        private ILoggerFacade m_Logger;
        
        public IRoomsControlable RoomsController { get; set; }
        public DevRoomMsgAnalyzer(ILoggerFacade logger, IRoomsControlable roomsController)
        {
            if (null == logger) throw new ArgumentNullException("logger");
            if (null == roomsController) throw new ArgumentNullException("roomsController");

            this.m_Logger = logger;
            this.RoomsController = roomsController;
        }

        public string CreateRecordForChatData(string strMsgSid, string strSenderSid, string strRoomSid, ContentType contentType,
            string strContent, long lSendTime)
        {
            var MessageCollection = ConnectionHelper.DB.GetCollection<MessageEntity>("MessageEntity");
            var NewMessage = new MessageEntity(strMsgSid, strRoomSid, strContent, (int)MsgStatus.Received, (int)contentType,
                                strSenderSid, lSendTime, null);

            MessageCollection.Save(NewMessage);
            return NewMessage.Id.ToString();
        }

        public void AnalyzeChatData(string strMsgId, string strMsgSid, string strSenderSid, string strRoomSid, string strContent,
            long lSendTime, IList<string> arMemberSids, IList<string> arActiveMemberSids, ContentType contentType)
        {
            if (false == this.RoomsController.ExistsRoom(strRoomSid))
            {
                var RoomCollection = ConnectionHelper.DB.GetCollection<RoomEntity>("RoomEntity");
                var NewRoom = new RoomEntity(strRoomSid, AuthRepository.MQKeyInfo.UserSid, arMemberSids, arActiveMemberSids, 1, strContent, lSendTime, null);
                RoomCollection.Save(NewRoom);

                this.RoomsController.AddRoom(strRoomSid, arMemberSids, 1, strContent, lSendTime, null);
            }
            else
            {
                this.RoomsController.UpdateRoom(strMsgId, strSenderSid, strMsgSid, arActiveMemberSids, strRoomSid, 1, contentType, strContent, lSendTime, null);
            }
        }

        public void AnalyzeRead(ReceivedReadMsg rcvdReadMsg)
        {
            this.RoomsController.ReadMessagesForRoom(rcvdReadMsg);
        }
       
        public void SaveUserIfNotExist(string strUserId, string strUserSid, string strNickName, IList<string> arInterests)
        {
            var UserCollection = ConnectionHelper.DB.GetCollection<UserEntity>("UserEntity");
            var FindUserQuery = Query<UserEntity>.EQ(u => u.Sid, strUserSid);
            var FindedUser = UserCollection.FindOne(FindUserQuery);

            if (null == FindedUser)
            {
                var NewUser = new UserEntity(strUserId, strUserSid, strNickName, arInterests);
                UserCollection.Save(NewUser);
            }
            else
            {
                //var UpdateUserQuery = Update<UserEntity>.Set(u => u.InterestIds, arInterests);
                //UserCollection.Update(FindUserQuery, UpdateUserQuery);
            }
        }
    }
}
