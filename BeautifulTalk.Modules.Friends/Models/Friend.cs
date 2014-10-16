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
        private byte[] m_Thumbnail;
        private string m_strNickName;
        private string m_strComment;
        private Brush m_AnonymousBackground;
        public string Sid { get; set; }
        public string Id 
        {
            get { return this.m_Id; }
            set { SetProperty(ref this.m_Id, value); }
        }
        public byte[] Thumbnail 
        {
            get { return this.m_Thumbnail; }
            set { SetProperty(ref m_Thumbnail, value); }
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

        public Brush AnonymousBackground
        {
            get { return this.m_AnonymousBackground; }
            set { SetProperty(ref this.m_AnonymousBackground, value); }
        }

        public Friend(byte[] thumbnailSource, string strId, string strSid, string strNickName, string strComment)
        {
            this.Thumbnail = thumbnailSource;
            this.Id = strId;
            this.Sid = strSid;
            this.NickName = strNickName;
            this.Comment = strComment;

            if (null == thumbnailSource) { this.AnonymousBackground = ColorGenerator.Instance.GetRandomBrush(); }
        }
    }
}
