using BeautifulTalk.Modules.DetailInfo.Views;
using BeautifulTalkInfrastructure.RegionNames;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.DetailInfo
{
    public class DetailInfoModuleInit : IModule
    {
        private IUnityContainer m_Container;
        private IRegionManager m_RegionManager;

        public DetailInfoModuleInit(IUnityContainer container, IRegionManager regionManager)
        {
            this.m_Container = container;
            this.m_RegionManager = regionManager;
        }
        
        public void Initialize()
        {
            this.m_RegionManager.RegisterViewWithRegion(BusinessRegionNames.AdditionalRegion, () => this.m_Container.Resolve<DetailInfoView>());
        }
    }
}
