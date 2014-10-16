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
        public string ThumbnailPath { get; private set; }
        public Brush AnonymousThumbnail { get; private set; }
        public RecommendFriendSummary(string strThumbnailPath, string strUserID, string strUserSID, string strComment, string strNickName)
        {
            this.ThumbnailPath = strThumbnailPath;
            this.UserID = strUserID;
            this.UserSID = strUserSID;
            this.Comment = strComment;
            this.NickName = strNickName;

            if (string.IsNullOrEmpty(strThumbnailPath)) { this.AnonymousThumbnail = ColorGenerator.Instance.GetRandomBrush(); }
        }
    }
}
