using BeautifulTalk.Modules.Chatting.Models;
using BeautifulTalkInfrastructure.DataModels;
using Microsoft.Practices.Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Chatting.Services.Mocks
{
    public class MockSendMessageService : ISendMessageService
    {
        public SendingMsgResult SendMessage(SendingMsg sendMsg)
        {
            throw new NotImplementedException();
        }
    }
}
