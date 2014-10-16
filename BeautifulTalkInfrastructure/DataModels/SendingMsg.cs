using BeautifulTalkInfrastructure.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalkInfrastructure.DataModels
{
    public class SendingMsg
    {
        public IList<string> ToSIDs { get; set; }
        public string RoomSID { get; set; }
        public int ContentType { get; set; }
        public string Content { get; set; }
        public string FromSID { get; set; }
        public ICommunicationMsgInfo CommunicationMsg { get; set; }
        public SendingMsg(IList<string> strToSIDs, string strRoomSID, int nContentType, string strContent, string strFromSID, ICommunicationMsgInfo communicationMsg)
        {
            this.ToSIDs = strToSIDs;
            this.RoomSID = strRoomSID;
            this.ContentType = nContentType;
            this.Content = strContent;
            this.FromSID = strFromSID;
            this.CommunicationMsg = communicationMsg;
        }
    }
}
