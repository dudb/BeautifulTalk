using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BeautifulTalk.Modules.Chatting.Factories
{
    public interface ICreateMsgContent
    {
        DependencyObject Create();
    }
}
