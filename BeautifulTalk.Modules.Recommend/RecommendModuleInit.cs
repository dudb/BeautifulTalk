using BeautifulTalk.Modules.Recommend.Services;
using BeautifulTalk.Modules.Recommend.Services.Mocks;
using BeautifulTalk.Modules.Recommend.Views;
using BeautifulTalkInfrastructure.RegionNames;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Recommend
{
    public class RecommendModuleInit : IModule
    {
        private IUnityContainer m_Container;
        private IRegionManager m_RegionManager;

        public RecommendModuleInit(IUnityContainer container, IRegionManager regionManager)
        {
            this.m_Container = container;
            this.m_RegionManager = regionManager;
        }
        public void Initialize()
        {
            //Mock
            //this.m_Container.RegisterType<ICollectRecommendUsers, MockCollectRecommendUsers>();
            //Real
            this.m_Container.RegisterType<ICollectRecommendUsers, CollectRecommendUsers>();

            this.m_RegionManager.RegisterViewWithRegion(BusinessRegionNames.TabbingRegion, () => this.m_Container.Resolve<RecommendView>());
        }
    }
}
