using BeautifulTalkInfrastructure.Generators;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BeautifulTalk.Modules.Friends.Models
{
    public class RecommendFriendSummary : BindableBase
    {
        public string UserID { get; private set; }
        public string UserSID { get; private set; }
        public string Comment { get; private set; }
        public string NickName { get; private set; }
        public byte[] Thumbnail { get; private set; }
        public Brush AnonymousBackground { get; private set; }
        public RecommendFriendSummary(byte[] arThumbnail, string strUserID, string strUserSID, string strComment, string strNickName)
        {
            this.Thumbnail = arThumbnail;
            this.UserID = strUserID;
            this.UserSID = strUserSID;
            this.Comment = strComment;
            this.NickName = strNickName;

            if (null == arThumbnail) { this.AnonymousBackground = ColorGenerator.Instance.GetRandomBrush(); }
        }
    }
}
