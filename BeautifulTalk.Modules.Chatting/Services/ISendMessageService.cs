using BeautifulTalk.Modules.Chatting.Models;
using BeautifulTalkInfrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Chatting.Services
{
    public interface ISendMessageService
    {
        SendingMsgResult SendMessage(SendingMsg sendingMsg);
    }
}
