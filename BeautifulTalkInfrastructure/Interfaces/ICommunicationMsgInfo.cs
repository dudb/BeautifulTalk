using BeautifulTalkInfrastructure.ProtocolFormat;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalkInfrastructure.Interfaces
{
    public interface ICommunicationMsgInfo
    {
        long SendTime { get; set; }
        MsgStatus MsgStatus { get; set; }
        string Id { get; set; }
        string Sid { get; set; }
        string RoomSid { get; set; }
    }
}
