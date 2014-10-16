using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Rooms.Services.Mocks
{
    public class MockRoomMsgAnalyzer : IRoomMsgAnalyzable
    {
        public void AnalyzeChatData(string strMsgId, string strMsgSid, string strSenderSid, string strRoomSid, string strContent, long lSendTime, IList<string> arMemberSids, IList<string> arActiveMemberSids, BeautifulTalkInfrastructure.ProtocolFormat.ContentType contentType)
        {
            throw new NotImplementedException();
        }

        public void AnalyzeRead()
        {
            throw new NotImplementedException();
        }

        public string CreateRecordForChatData(string strMsgSid, string strSenderSid, string strRoomSid, BeautifulTalkInfrastructure.ProtocolFormat.ContentType contentType, string strContent, long lSendTime)
        {
            throw new NotImplementedException();
        }

        public void SaveUserIfNotExist(string strUserId, string strUserSid, string strNickName, IList<string> arInterests)
        {
            throw new NotImplementedException();
        }
    }
}
