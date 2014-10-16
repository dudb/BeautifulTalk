using BeautifulTalkInfrastructure.Interfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BeautifulTalkInfrastructure.ShellBases
{
    public class MinMaxClosableShellViewModelBase : BindableBase, IMinExpColCloseWindow<IWindowBehavior>
    {
        public DelegateCommand<IWindowBehavior> MinimizeWindowBtnClickedCommand { get; private set; }
        public DelegateCommand<IWindowBehavior> ExpandCollapseWindowBtnClickedCommand { get; private set; }
        public DelegateCommand<IWindowBehavior> CloseWindowBtnClickedCommand { get; private set; }
        public MinMaxClosableShellViewModelBase()
        {
            this.MinimizeWindowBtnClickedCommand = new DelegateCommand<IWindowBehavior>(MinimizeWindow);
            this.ExpandCollapseWindowBtnClickedCommand = new DelegateCommand<IWindowBehavior>(ExpandCollapseWindow);
            this.CloseWindowBtnClickedCommand = new DelegateCommand<IWindowBehavior>(CloseWindow);
        }
        
        public void MinimizeWindow(IWindowBehavior window)
        {
            if (null == window) throw new ArgumentNullException("window");

            window.CurrentWindowState = WindowState.Minimized;
        }
        public void ExpandCollapseWindow(IWindowBehavior window)
        {
            if (null == window) throw new ArgumentNullException("window");

            WindowState CurrentWindowState = window.CurrentWindowState;

            if (WindowState.Normal == CurrentWindowState)
            {
                window.CurrentWindowState = WindowState.Maximized;   
            }
            else
            {
                window.CurrentWindowState = WindowState.Normal;
            }
        }
        public virtual void CloseWindow(IWindowBehavior window)
        {
            if (null == window) throw new ArgumentNullException("window");

            window.CloseWindow();
        }
    }
}
