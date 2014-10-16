using BeautifulTalkInfrastructure.Interfaces;
using BeautifulTalkInfrastructure.ProtocolFormat;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Chatting.Models
{
    public abstract class CommunicationMsg : Msg, ICommunicationMsgInfo
    {
        private long m_lSendTime;
        private MsgStatus m_nMsgStatus;
        public string Id { get; set; }
        public string Sid { get; set; }
        public string RoomSid { get; set; }
        public long SendTime
        {
            get { return this.m_lSendTime; }
            set { SetProperty(ref this.m_lSendTime, value); }
        }

        //Failed = 0,Sending = 1,Sent = 2,Read = 3,Received = 4
        public MsgStatus MsgStatus
        {
            get { return this.m_nMsgStatus; }
            set { SetProperty(ref this.m_nMsgStatus, value); }
        }

        public CommunicationMsg(string strId, string strSid, string strRoomSid, long lSendTime, MsgStatus nMsgStatus, string strContent, ContentType contentType)
            : base(strContent, contentType)
        {
            this.Id = strId;
            this.Sid = strSid;
            this.RoomSid = strRoomSid;
            this.SendTime = lSendTime;
            this.MsgStatus = nMsgStatus;
        }
    }
}
