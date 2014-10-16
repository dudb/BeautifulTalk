using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Chatting.Views
{
    public interface IChattingViewBehavior
    {
        void ScrollIntoView(object targetObject);
        void ClearInput();
    }
}
