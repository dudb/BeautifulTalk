using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulDB.Entities
{
    public class RoomEntity
    {
        public ObjectId Id { get; private set; }
        public string UserSid { get; set; }
        public string Sid { get; set; }
        public IList<string> MemberSids { get; set; }
        public IList<string> ActiveMemberSids { get; set; }
        public int UnReadMsgCount { get; set; }
        public string LastMsgSummary { get; set; }
        public long LastMsgDate { get; set; }
        public string ThumbnailPath { get; set; }

        public RoomEntity(string strSid, string strUserSid, IList<string> arMemberSids, IList<string> arActiveMemberSids, int nUnReadMsgCount, 
            string strLastMsgSummary, long lLastMsgDate, string strThumbnailPath)
        {
            this.Sid = strSid;
            this.UserSid = strUserSid;
            this.MemberSids = arMemberSids;
            this.ActiveMemberSids = arActiveMemberSids;
            this.UnReadMsgCount = nUnReadMsgCount;
            this.LastMsgSummary = strLastMsgSummary;
            this.LastMsgDate = lLastMsgDate;
            this.ThumbnailPath = strThumbnailPath;
        }
    }
}
