using BeautifulTalkInfrastructure.ProtocolFormat;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Chatting.Models
{
    public class OpponentMsg : ChatMsg
    {
        public OpponentMsg(string strId, string strSid, string strRoomSid, string strContent, ContentType contentType, long lSendTime,
            MsgStatus msgStatus, int nReadMembersCount, string strSender, byte[] thumbNail)
            : base(strId, strSid, strRoomSid, strContent, contentType, lSendTime, msgStatus, nReadMembersCount, strSender, thumbNail)
        {
        }
    }
}
