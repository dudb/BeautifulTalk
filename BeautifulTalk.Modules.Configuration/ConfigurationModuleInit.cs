using BeautifulTalk.Modules.Configuration.Services;
using BeautifulTalk.Modules.Configuration.Views;
using BeautifulTalkInfrastructure.RegionNames;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Configuration
{
    public class ConfigurationModuleInit : IModule
    {
        private IUnityContainer m_Container;
        private IRegionManager m_RegionManager;

        public ConfigurationModuleInit(IUnityContainer container, IRegionManager regionManager)
        {
            this.m_Container = container;
            this.m_RegionManager = regionManager;
        }
        public void Initialize()
        {
            this.m_Container.RegisterType<ICollectConfigurationInfo, CollectConfigurationInfo>();
            this.m_RegionManager.RegisterViewWithRegion(BusinessRegionNames.TabbingRegion, () => this.m_Container.Resolve<ConfigurationView>());
        }
    }
}
