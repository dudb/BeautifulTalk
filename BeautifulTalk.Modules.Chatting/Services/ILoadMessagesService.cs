using BeautifulTalk.Modules.Chatting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Chatting.Services
{
    public interface ILoadMessagesService
    {
        /// <summary>
        /// if lCriterion is zero, it will return recent messages.
        /// </summary>
        IEnumerable<Msg> LoadMessages(string strRoomSID, long lCriterion);
    }
}
