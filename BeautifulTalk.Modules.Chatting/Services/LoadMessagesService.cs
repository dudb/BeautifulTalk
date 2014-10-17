using BeautifulDB.Entities;
using BeautifulDB.Helpers;
using BeautifulTalk.Modules.Chatting.Models;
using BeautifulTalkInfrastructure.DataModels;
using BeautifulTalkInfrastructure.Generators;
using BeautifulTalkInfrastructure.Logger;
using BeautifulTalkInfrastructure.ProtocolFormat;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BeautifulTalk.Modules.Chatting.Services
{
    public class LoadMessagesService : ILoadMessagesService
    {
        private readonly string m_strMySid;
        private readonly IDictionary<string, Brush> m_AnonymousThumbnailDictionary;
        public LoadMessagesService(string strMySid, IDictionary<string, Brush> anonymousThumbnailDictionary)
        {
            this.m_strMySid = strMySid;
            this.m_AnonymousThumbnailDictionary = anonymousThumbnailDictionary;
        }
        public IEnumerable<Msg> LoadMessages(string strRoomSID, long lCriterion)
        {
            IList<Msg> Msgs = new List<Msg>();

            try
            {
                if (0 == lCriterion)
                {
                    this.LoadRecentMessages(strRoomSID, Msgs);
                }
                else
                {
                    this.LoadMessagesLessThan(lCriterion, strRoomSID, Msgs);
                }
            }
            catch (NullReferenceException nullRefException)
            {
                GlobalLogger.Log(nullRefException.Message);
                throw nullRefException;
            }
            catch (Exception unExpectedException)
            {
                GlobalLogger.Log(unExpectedException.Message);
                throw unExpectedException;
            }

            return Msgs;
        }

        //This method will occur only once when a chatting room will be loaded.
        private void LoadRecentMessages(string strRoomSID, IList<Msg> msgs)
        {
            var FindSuccessMessagesQuery = Query.And(
                Query<MessageEntity>.EQ(m => m.RoomSid, strRoomSID),
                Query<MessageEntity>.NE(m => m.State, (int)MsgStatus.Failed),
                Query<MessageEntity>.NE(m => m.State, (int)MsgStatus.Sending));

            var FindFailedMessageQuery = Query.And(
                Query<MessageEntity>.EQ(m => m.RoomSid, strRoomSID),
                Query.Or(
                    Query<MessageEntity>.EQ(m => m.State, (int)MsgStatus.Failed),
                    Query<MessageEntity>.EQ(m => m.State, (int)MsgStatus.Sending))
                );

            var MessageCollection = ConnectionHelper.DB.GetCollection<MessageEntity>("MessageEntity");
            var FindedSuccessMessages = MessageCollection.Find(FindSuccessMessagesQuery).SetSortOrder(SortBy.Descending("SendTime")).SetLimit(20);
            var FindedFailedMessages = MessageCollection.Find(FindFailedMessageQuery).OrderByDescending(m => m.Id);
            var FindedMessages = FindedFailedMessages.Concat(FindedSuccessMessages);

            foreach (MessageEntity msgEntity in FindedMessages)
            {
                Msg WillAddMsg;
                string strSenderSID = msgEntity.SenderSid;
                MsgStatus MsgState = (MsgStatus)msgEntity.State;
                if (MsgStatus.Sending == MsgState) { MsgState = MsgStatus.Failed; }

                ContentType ContentType = (ContentType)msgEntity.Type;
                int nReadMembersCount = (null == msgEntity.ReadMembers ? 0 : msgEntity.ReadMembers.Count);

                var UserCollection = ConnectionHelper.DB.GetCollection<UserEntity>("UserEntity");
                var FindUserQuery = Query<UserEntity>.EQ(u => u.Sid, strSenderSID);
                var FindedUser = UserCollection.FindOne(FindUserQuery);

                if (null != FindedUser)
                {
                    if (this.m_strMySid == strSenderSID)
                    {
                        WillAddMsg = new ChatMsg(msgEntity.Id.ToString(), msgEntity.Sid, msgEntity.RoomSid, msgEntity.Content, ContentType,
                            msgEntity.SendTime, MsgState, nReadMembersCount, FindedUser.NickName, null, this.m_AnonymousThumbnailDictionary[this.m_strMySid]);
                    }
                    else
                    {
                        if (false == this.m_AnonymousThumbnailDictionary.ContainsKey(strSenderSID))
                        {
                            this.m_AnonymousThumbnailDictionary.Add(strSenderSID, ColorGenerator.Instance.GetRandomBrush());
                        }

                        WillAddMsg = new OpponentMsg(msgEntity.Id.ToString(), msgEntity.Sid, msgEntity.RoomSid, msgEntity.Content, ContentType,
                            msgEntity.SendTime, MsgState, nReadMembersCount, FindedUser.NickName, null, this.m_AnonymousThumbnailDictionary[strSenderSID]);
                    }

                    msgs.Add(WillAddMsg);
                }
            }
        }

        private void LoadMessagesLessThan(long lCriterion, string strRoomSID, IList<Msg> msgs)
        {
            var FindSuccessMessagesQuery = Query.And(
                Query<MessageEntity>.EQ(m => m.RoomSid, strRoomSID),
                Query<MessageEntity>.LT(m => m.SendTime, lCriterion),
                Query<MessageEntity>.NE(m => m.State, 0),
                Query<MessageEntity>.NE(m => m.State, 1));

            var MessageCollection = ConnectionHelper.DB.GetCollection<MessageEntity>("MessageEntity");
            var FindedSuccessMessages = MessageCollection.Find(FindSuccessMessagesQuery).SetSortOrder(SortBy.Descending("SendTime")).SetLimit(20);

            foreach (MessageEntity msgEntity in FindedSuccessMessages)
            {
                Msg WillAddMsg;
                string strSenderSID = msgEntity.SenderSid;
                MsgStatus MsgState = (MsgStatus)msgEntity.State;
                ContentType ContentType = (ContentType)msgEntity.Type;
                int nReadMembersCount = (null == msgEntity.ReadMembers ? 0 : msgEntity.ReadMembers.Count);

                var UserCollection = ConnectionHelper.DB.GetCollection<UserEntity>("UserEntity");
                var FindUserQuery = Query<UserEntity>.EQ(u => u.Sid, strSenderSID);
                var FindedUser = UserCollection.FindOne(FindUserQuery);

                if (null != FindedUser)
                {
                    if (this.m_strMySid == strSenderSID)
                    {
                        WillAddMsg = new ChatMsg(msgEntity.Id.ToString(), msgEntity.Sid, msgEntity.RoomSid, msgEntity.Content, ContentType,
                            msgEntity.SendTime, MsgState, nReadMembersCount, FindedUser.NickName, null, this.m_AnonymousThumbnailDictionary[this.m_strMySid]);
                    }
                    else
                    {
                        if (false == this.m_AnonymousThumbnailDictionary.ContainsKey(strSenderSID))
                        {
                            this.m_AnonymousThumbnailDictionary.Add(strSenderSID, ColorGenerator.Instance.GetRandomBrush());
                        }

                        WillAddMsg = new OpponentMsg(msgEntity.Id.ToString(), msgEntity.Sid, msgEntity.RoomSid, msgEntity.Content, ContentType,
                            msgEntity.SendTime, MsgState, nReadMembersCount, FindedUser.NickName, null, this.m_AnonymousThumbnailDictionary[strSenderSID]);
                    }

                    msgs.Add(WillAddMsg);
                }
            }
        }


        public IList<UnReadMsg> LoadUnReadMessages(string strRoomSID)
        {
            IList<UnReadMsg> UnReadMsgs = new List<UnReadMsg>();
            var MessageCollection = ConnectionHelper.DB.GetCollection<MessageEntity>("MessageEntity");
            var FindUnReadMessagesQuery = Query.And(
                Query<MessageEntity>.EQ(m => m.RoomSid, strRoomSID),
                Query<MessageEntity>.EQ(m => m.State, (int)MsgStatus.Received));

            var FindedUnReadMessages = MessageCollection.Find(FindUnReadMessagesQuery);
            foreach (MessageEntity unReadMsg in FindedUnReadMessages)
            {
                UnReadMsgs.Add(new UnReadMsg(unReadMsg.Id, unReadMsg.ReadMembers, unReadMsg.Sid, unReadMsg.RoomSid, this.m_strMySid));
            }

            return UnReadMsgs;
        }
    }
}
