using BeautifulTalkInfrastructure.ProtocolFormat;
using MongoDB.Bson;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Rooms.Services
{
    public interface IRoomMsgAnalyzable
    {
        void AnalyzeChatData(string strMsgId, string strMsgSid, string strSenderSid, string strRoomSid, string strContent,
            long lSendTime, IList<string> arMemberSids, IList<string> arActiveMemberSids, ContentType contentType);
        void AnalyzeRead();
        string CreateRecordForChatData(string strMsgSid, string strSenderSid, string strRoomSid, ContentType contentType,
            string strContent, long lSendTime);
        void SaveUserIfNotExist(string strUserId, string strUserSid, string strNickName, IList<string> arInterests);
    }
}
