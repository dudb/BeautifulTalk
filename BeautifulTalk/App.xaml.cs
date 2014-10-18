using BeautifulTalk.Tray;
using BeautifulTalkInfrastructure.Logger;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
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

            string strMutexName = string.Format("{0}.{1}.{2}", Environment.MachineName, Environment.UserName, "BeautifulTalk");

            bool bCreateNew;
            var mutex = new Mutex(true, strMutexName, out bCreateNew);

            if (false == bCreateNew)
            {
                Application.Current.Shutdown();
            }

            this.InitializeInfra();
        }

        private void InitializeInfra()
        {
            TalkBootstrapper bootStrapper = new TalkBootstrapper();
            bootStrapper.Run();

            m_TrayIcon = new AppTrayView();
        }
    }
}
