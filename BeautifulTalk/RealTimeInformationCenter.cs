using BeautifulTalk.Views;
using BeautifulTalkInfrastructure.AliveInformation;
using BeautifulTalkInfrastructure.ModuleNames;
using BeautifulTalkInfrastructure.PubSubEvents;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BeautifulTalk
{
    public class RealTimeInformationCenter : IModule
    {
        private ILoggerFacade m_Logger;
        private IEventAggregator m_EventAggregator;
        private IUnityContainer m_Container;
        private IRegionManager m_RegionManager;

        private SubscriptionToken m_StartupBizShellEventToken;

        public RealTimeInformationCenter(IUnityContainer unityContainer, ILoggerFacade logger, 
            IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            if (null == unityContainer) throw new ArgumentNullException("unityContainer");
            if (null == logger)             throw new ArgumentNullException("logger");
            if (null == eventAggregator)    throw new ArgumentNullException("eventAggregator");
            if (null == regionManager) throw new ArgumentNullException("regionManager");
            
            this.m_Logger = logger;
            this.m_EventAggregator = eventAggregator;
            this.m_Container = unityContainer;
            this.m_RegionManager = regionManager;
        }

        public void Initialize()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StartupBusinessShellEvent StartupBizShellEvent = this.m_EventAggregator.GetEvent<StartupBusinessShellEvent>();

            if (null != this.m_StartupBizShellEventToken)
            {
                StartupBizShellEvent.Unsubscribe(this.m_StartupBizShellEventToken);
            }

            this.m_StartupBizShellEventToken = StartupBizShellEvent.Subscribe(StartupBusinessShell, ThreadOption.BackgroundThread, true, null);
        }

        private void StartupBusinessShell(object objParameter)
        {
            Application.Current.Dispatcher.Invoke(ShowBusinessShell);
        }

        private void ShowBusinessShell()
        {
            var BusinessShellView = this.m_Container.Resolve<BusinessShellView>();
            BusinessShellView.Show();
            RegionManager.SetRegionManager(BusinessShellView, this.m_Container.Resolve<IRegionManager>());
            
            this.m_Container.Resolve<IModuleManager>().LoadModule(WellKnownModuleNames.FriendsModuleInit);
            this.m_Container.Resolve<IModuleManager>().LoadModule(WellKnownModuleNames.RoomsModuleInit);
            this.m_Container.Resolve<IModuleManager>().LoadModule(WellKnownModuleNames.ConfigurationModuleInit);
            this.m_Container.Resolve<IModuleManager>().LoadModule(WellKnownModuleNames.DetailInfoModuleInit);

            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = BusinessShellView;
        }
    }
}
