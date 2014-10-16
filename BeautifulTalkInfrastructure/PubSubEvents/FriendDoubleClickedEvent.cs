using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalkInfrastructure.PubSubEvents
{
    public class FriendDoubleClickParams
    {
        public string Sid { get; set; }
        public IList<string> Members { get; set; }
        public int UnReadMsgCount { get; set; }
        public string LastMsgSummary { get; set; }
        public long LastMsgDate { get; set; }
        public string ThumbnailPath { get;set; }

        public FriendDoubleClickParams(string strSid, IList<string> arMembers, int nUnReadMsgCount, string strLastMsgSummary, long lLastMsgDate, string strThumbnailPath)
        {
            this.Sid = strSid;
            this.Members = arMembers;
            this.UnReadMsgCount = nUnReadMsgCount;
            this.LastMsgSummary = strLastMsgSummary;
            this.LastMsgDate = lLastMsgDate;
            this.ThumbnailPath = strThumbnailPath;
        }
    }
    public class FriendDoubleClickedEvent : PubSubEvent<FriendDoubleClickParams>
    {
    }
}
