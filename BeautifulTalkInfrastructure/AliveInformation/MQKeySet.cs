using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BeautifulTalkInfrastructure.AliveInformation
{
    public class MQKeySet
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string NickName { get; set; }
        public string Account { get; set; }
        public string MqSid { get; set; }
        public string MqKey { get; set; }
        public int Type { get; set; }
        public string ThumbnailPath { get; set; }
        public string UserSid { get; set; }
        public List<string> Interests { get; set; }
    }
}
