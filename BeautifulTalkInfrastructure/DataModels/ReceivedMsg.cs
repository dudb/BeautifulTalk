using BeautifulTalkInfrastructure.ProtocolFormat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalkInfrastructure.DataModels
{
    public class ReceivedMsg
    {
        public string Id { get; set; }
        public string Sid { get; set; }
        public string SenderSid { get; set; }
        public string RoomSid { get; set; }
        public ContentType ContentType { get; set; }
        public string Content { get; set; }
        public long SendTime { get; set; }
        public int ReadMembersCount { get; set; }
        public string ThumbnailPath { get; set; }
        public bool IsActivatedView { get; set; }

        public ReceivedMsg(string strId, string strSenderSid, string strRoomSid, string strSid, ContentType contentType, string strContent, 
            long lSendTime, int nReadMembersCount, string strThumbnailPath, bool bIsActivatedView)
        {
            this.Id = strId;
            this.SenderSid = strSenderSid;
            this.RoomSid = strRoomSid;
            this.Sid = strSid;
            this.ContentType = contentType;
            this.Content = strContent;
            this.SendTime = lSendTime;
            this.ReadMembersCount = nReadMembersCount;
            this.ThumbnailPath = strThumbnailPath;
            this.IsActivatedView = bIsActivatedView;
        }
    }
}
