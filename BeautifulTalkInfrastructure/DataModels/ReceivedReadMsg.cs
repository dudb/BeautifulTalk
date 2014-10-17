using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalkInfrastructure.DataModels
{
    public class ReceivedReadMsg
    {
        public string RoomSid { get; set; }
        public string MsgSids { get; set; }
        public string SenderSid { get; set; }

        public ReceivedReadMsg(string strRoomSid, string strMsgSids, string strSenderSid)
        {
            this.RoomSid = strRoomSid;
            this.MsgSids = strMsgSids;
            this.SenderSid = strSenderSid;
        }
    }
}
