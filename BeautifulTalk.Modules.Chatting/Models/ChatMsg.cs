using BeautifulTalkInfrastructure.Generators;
using BeautifulTalkInfrastructure.ProtocolFormat;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BeautifulTalk.Modules.Chatting.Models
{
    public class ChatMsg : CommunicationMsg
    {
        private int m_ReadMembersCount;
        public int ReadMembersCount
        {
            get { return this.m_ReadMembersCount; }
            set { SetProperty(ref this.m_ReadMembersCount, value); }
        }
        public string Sender { get; set; }
        public string ThumbnailPath { get; set; }
        public Brush AnonymousThumbnail { get; private set; }

        public ChatMsg(string strId, string strSid, string strRoomSid, string strContent, ContentType contentType, long lSendTime,
            MsgStatus msgStatus, int nReadMembersCount, string strSender, string strThumbNailPath, Brush anonymouseThumbnail)
            : base(strId, strSid, strRoomSid, lSendTime, msgStatus, strContent, contentType)
        {
            this.ReadMembersCount = nReadMembersCount;
            this.Sender = strSender;
            this.ThumbnailPath = strThumbNailPath;
            this.AnonymousThumbnail = anonymouseThumbnail;
        }
    }
}
