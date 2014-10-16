using BeautifulTalkInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BeautifulTalkInfrastructure.ShellBases
{
    public class ShellViewBase : Window, IWindowBehavior
    {
        public ShellViewBase()
        { 
        
        }
        public WindowState CurrentWindowState
        {
            get { return this.WindowState; }
            set { this.WindowState = value; }
        }

        public void CloseWindow()
        {
            this.Close();
        }
    }
}
