using BeautifulTalkInfrastructure.DataModels;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalkInfrastructure.Interfaces
{
    public interface IChattingViewModel
    {
        DelegateCommand<ReceivedMsg> ReceiveChatMsgCommand { get; }
        DelegateCommand<ReceivedReadMsg> ReceiveReadMsgCommand { get; }
        void RequestReadMsgs();
    }
}
