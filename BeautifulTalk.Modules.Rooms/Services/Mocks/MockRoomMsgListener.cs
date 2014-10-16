using BeautifulTalkInfrastructure.AliveInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Rooms.Services.Mocks
{
    public class MockRoomMsgListener : IRoomMsgListener
    {
        public IRoomsTabHeaderInfoProvider RoomsView { get; set; }
        public IRoomMsgAnalyzable Analyzer { get; set; }

        public MockRoomMsgListener(IRoomMsgAnalyzable msgAnalyzer) 
        {
            if (null == msgAnalyzer) throw new ArgumentNullException("msgAnalyzer");

            this.Analyzer = msgAnalyzer;
        }
        public void StartListen()
        {

        }
    }
}