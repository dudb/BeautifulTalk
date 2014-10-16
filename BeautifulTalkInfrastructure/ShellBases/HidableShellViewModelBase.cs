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
    public class HidableShellViewModelBase : BindableBase, IHideWindow
    {
        public DelegateCommand HideWindowBtnClickedCommand { get; private set; }

        public HidableShellViewModelBase()
        {
            this.HideWindowBtnClickedCommand = new DelegateCommand(HideWindow);
        }

        public void HideWindow()
        {
            Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.IsActive).Hide();
        }
    }
}
