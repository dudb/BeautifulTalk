using BeautifulTalk.Tray;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace BeautifulTalk
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon m_TrayIcon;

        public App()
        { 
        
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            TalkBootstrapper bootStrapper = new TalkBootstrapper();
            bootStrapper.Run();

            m_TrayIcon = new AppTrayView();
        }
    }
}
