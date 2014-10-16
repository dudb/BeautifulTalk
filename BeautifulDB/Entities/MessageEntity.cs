using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulDB.Entities
{
    public class MessageEntity
    {
        public ObjectId Id { get; set; }
        public string Sid { get; set; }
        public string RoomSid{ get; set; }
        public string Content { get; set; }

        //MsgStatus Failed = 0, Sending = 1, Sent = 2,Read = 3, Received = 4
        public int State { get; set; }

        //0: Text, 1:Picture, 2:Media
        public int Type { get; set; }
        public string SenderSid { get; set; }
        public long SendTime{ get; set; }
        public IList<string> ReadMembers { get; set; }

        public MessageEntity(string strRoomSid, string strContent, int nState, int nType, string strSenderSid, long lSendTime, IList<string> arReadMembers)
        {
            this.RoomSid = strRoomSid;
            this.Content = strContent;
            this.State = nState;
            this.Type = nType;
            this.SenderSid = strSenderSid;
            this.SendTime = lSendTime;
            this.ReadMembers = arReadMembers;
        }

        public MessageEntity(string strMsgSid, string strRoomSid, string strContent, int nState, int nType, string strSenderSid, long lSendTime, IList<string> arReadMembers)
        {
            this.Sid = strMsgSid;
            this.RoomSid = strRoomSid;
            this.Content = strContent;
            this.State = nState;
            this.Type = nType;
            this.SenderSid = strSenderSid;
            this.SendTime = lSendTime;
            this.ReadMembers = arReadMembers;
        }
    }
}
