using BeautifulTalkInfrastructure.ProtocolFormat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Chatting.Models
{
    public class DateMsg : Msg
    {
        public long Date { get; private set; }

        public DateMsg(long lDate)
            : base(null, ContentType.Text)
        {
            this.Date = lDate;
        }
    }
}
