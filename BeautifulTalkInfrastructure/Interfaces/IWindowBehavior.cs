using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BeautifulTalkInfrastructure.Interfaces
{
    public interface IWindowBehavior
    {
        WindowState CurrentWindowState { get; set; }
        void CloseWindow();
    }
}
