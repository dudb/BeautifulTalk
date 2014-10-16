using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalkInfrastructure.Interfaces
{
    public interface IChattingShellView : IActiveChattingShellView
    {
        void ShowChattingShellView();
        IChattingViewModel ChattingViewModel { get; }
    }
}
