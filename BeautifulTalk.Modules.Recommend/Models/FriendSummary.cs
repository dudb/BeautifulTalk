using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BeautifulTalk.Modules.Recommend.Models
{
    public class FriendSummary
    {
        public string UserID { get; private set; }
        public string Comment { get; private set; }
        public string NickName { get; private set; }
        public byte[] Thumbnail { get; private set; }

        public FriendSummary(byte[] arThumbnail, string strUserID, string strComment, string strNickName)
        {
            this.Thumbnail = arThumbnail;
            this.UserID = strUserID;
            this.Comment = strComment;
            this.NickName = strNickName;
        }
    }
}
