using BeautifulTalkInfrastructure.Generators;
using BeautifulTalkInfrastructure.Interfaces;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BeautifulTalk.Modules.Rooms.Models
{
    public class Room : BindableBase
    {
        private Brush m_AnonymousThumbnail;
        private IList<string> m_ActiveMemberNames;
        private Int32 m_UnReadMsgCount;
        private string m_LastMsgSummary;
        private long m_LastMsgDate;
        private string m_ThumbnailPath;

        public string Sid { get; set; }
        public IList<string> ActiveMemberNames 
        {
            get { return this.m_ActiveMemberNames; }
            set { SetProperty(ref this.m_ActiveMemberNames, value); }
        }
        public Int32 UnReadMsgCount 
        {
            get { return this.m_UnReadMsgCount; }
            set { SetProperty(ref this.m_UnReadMsgCount, value); }
        }
        public string LastMsgSummary 
        {
            get { return this.m_LastMsgSummary; }
            set { SetProperty(ref this.m_LastMsgSummary, value); }
        }
        public long LastMsgDate 
        {
            get { return this.m_LastMsgDate; }
            set { SetProperty(ref this.m_LastMsgDate, value); }
        }
        public string ThumbnailPath 
        {
            get { return this.m_ThumbnailPath; }
            set { SetProperty(ref this.m_ThumbnailPath, value); }
        }

        public Brush AnonymousThumbnail
        {
            get { return this.m_AnonymousThumbnail; }
            set { SetProperty(ref this.m_AnonymousThumbnail, value); }
        }
        public Room(string strSid, IList<string> arActiveMemberNames, int nUnReadMsgCount, 
            string strLastMsgSummary, long lLastMsgDate, string strThumbnailPath)
        {
            this.Sid = strSid;
            this.ActiveMemberNames = arActiveMemberNames;
            this.UnReadMsgCount = nUnReadMsgCount;
            this.LastMsgSummary = strLastMsgSummary;
            this.LastMsgDate = lLastMsgDate;
            this.ThumbnailPath = strThumbnailPath;

            if (string.IsNullOrEmpty(strThumbnailPath)) { this.AnonymousThumbnail = ColorGenerator.Instance.GetRandomBrush(); }
        }
    }
}
