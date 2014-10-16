using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalkInfrastructure.DataModels
{
    public class SendingMsgResult
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string RoomSid { get; set; }
        public string MsgSid { get; set; }
        public long SentTime { get; set; }
    }
}
