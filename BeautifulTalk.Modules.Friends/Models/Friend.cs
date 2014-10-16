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
    public class Friend : BindableBase
    {
        private string m_Id;
        private string m_strThumbnailPath;
        private string m_strNickName;
        private string m_strComment;
        private Brush m_AnonymousThumbnail;
        public string Sid { get; set; }
        public string Id 
        {
            get { return this.m_Id; }
            set { SetProperty(ref this.m_Id, value); }
        }
        public string ThumbnailPath
        {
            get { return this.m_strThumbnailPath; }
            set { SetProperty(ref m_strThumbnailPath, value); }
        }
        public string NickName
        {
            get { return this.m_strNickName; }
            set { SetProperty(ref m_strNickName, value); }
        }
        public string Comment 
        {
            get { return this.m_strComment; }
            set { SetProperty(ref m_strComment, value); }
        }

        public Brush AnonymousThumbnail
        {
            get { return this.m_AnonymousThumbnail; }
            set { SetProperty(ref this.m_AnonymousThumbnail, value); }
        }

        public Friend(string strThumbnailPath, string strId, string strSid, string strNickName, string strComment)
        {
            this.ThumbnailPath = strThumbnailPath;
            this.Id = strId;
            this.Sid = strSid;
            this.NickName = strNickName;
            this.Comment = strComment;

            if (string.IsNullOrEmpty(strThumbnailPath)) { this.AnonymousThumbnail = ColorGenerator.Instance.GetRandomBrush(); }
        }
    }
}
