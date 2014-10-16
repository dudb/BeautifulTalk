using BeautifulTalkInfrastructure.AliveInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Rooms.Services
{
    public interface IRoomMsgListener
    {
        IRoomsTabHeaderInfoProvider RoomsView { get; set; }
        IRoomMsgAnalyzable Analyzer { get; set; }
        void StartListen();
    }
}
