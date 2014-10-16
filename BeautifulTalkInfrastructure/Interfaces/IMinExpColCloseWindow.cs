using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BeautifulTalkInfrastructure.Interfaces
{
    public interface IMinExpColCloseWindow<T>
    {
        void MinimizeWindow(T window);
        void ExpandCollapseWindow(T window);
        void CloseWindow(T window);
    }
}
