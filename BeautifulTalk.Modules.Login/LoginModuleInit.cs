using BeautifulTalk.Modules.Login.Services;
using BeautifulTalk.Modules.Login.Services.Client;
using BeautifulTalk.Modules.Login.Services.Client.Mocks;
using BeautifulTalk.Modules.Login.Views;
using BeautifulTalkInfrastructure.RegionNames;
using BeautifulTalkInfrastructure.ViewNames;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BeautifulTalk.Modules.Login
{
    public class LoginModuleInit : IModule
    {
        private readonly ILoggerFacade m_Logger;
        private readonly IUnityContainer m_Container;
        private readonly IRegionManager m_RegionManager;

        public LoginModuleInit(ILoggerFacade logger, IUnityContainer container, IRegionManager regionManager)
        {
            if (null == logger) throw new ArgumentNullException("logger");
            if (null == container) throw new ArgumentNullException("container");
            if (null == regionManager) throw new ArgumentNullException("regionManager");

            this.m_Logger = logger;
            this.m_Container = container;
            this.m_RegionManager = regionManager;
        }

        public void Initialize()
        {
            //Mock
            this.m_Container.RegisterType<ITrackSuccessConnectionService, MockTrackConnectionService>();
            //this.m_Container.RegisterType<IRequestAccessInfoService, MockRequestAccessInfoService>();
            //this.m_Container.RegisterType<IRequestMQKeysService, MockRequestMQKeysService>();
            //this.m_Container.RegisterType<ICollectInterestCategoriesService, MockCollectInterestCategoriesService>();

            //Real
            //this.m_Container.RegisterType<ITrackSuccessConnectionService, TrackConnectionService>();
            this.m_Container.RegisterType<IRequestAccessInfoService, RequestAccessInfoService>();
            this.m_Container.RegisterType<IRequestMQKeysService, RequestMQKeysService>();
            this.m_Container.RegisterType<ICollectInterestCategoriesService, CollectInterestCategoriesService>();

            this.m_Container.RegisterType<ILoginTriableService, LoginRequestService>();
            this.m_Container.RegisterType<object, RequiredInfoView>(LoginViewNames.RequiredInfoView);
            this.m_Container.RegisterType<object, InterestView>(LoginViewNames.InterestView);
            this.m_RegionManager.RegisterViewWithRegion(LoginRegionNames.LoginRegion, () => this.m_Container.Resolve<LoginView>());
        }
    }
}
