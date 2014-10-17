using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalkInfrastructure.DataModels
{
    public class UnReadMsg
    {
        public ObjectId Id { get; set; }
        public IList<string> ReadMembers { get; set; }
        public string Sid { get; set; }
        public string RoomSid { get; set; }
        public string FromSid { get; set; }
        public UnReadMsg(ObjectId id, IList<string> arReadMembers, string strSid, string strRoomSid, string strFromSid)
        {
            this.Id = id;
            this.ReadMembers = arReadMembers;
            this.Sid = strSid;
            this.RoomSid = strRoomSid;
            this.FromSid = strFromSid;
        }
    }
}
