using BeautifulTalkInfrastructure.DataModels;
using BeautifulTalkInfrastructure.ProtocolFormat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BeautifulTalkInfrastructure.Interfaces
{
    public interface IRoomsControlable : ISearchRoom
    {
        void CreateChattingShellOrActivate(string strRoomSID);
        void RemoveChattingShell(string strRoomSID);
        void AddRoom(string strSid, IList<string> arMembers, int nUnReadMsgCount, string strLastMsgSummary, long lLastMsgDate, string strThumbnailPath);
        void UpdateRoom(string strMsgId, string strSenderSid, string strMsgSid, IList<string> arActiveMemberSids, string strSid, int nUnReadMsgCount, ContentType contentType,
            string strContent, long lLastMsgDate, string strThumbnailPath);
        void ReadMessagesForRoom(ReceivedReadMsg rcvdReadMsg);
        void ResetUnReadCountForRoom(string strRoomSID);
        void UpdateLastMsg(string strRoomSID, string strContent, long lSendTime);
    }
}
