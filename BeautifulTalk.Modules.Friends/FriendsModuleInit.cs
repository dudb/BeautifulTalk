using BeautifulTalk.Modules.Friends.Services;
using BeautifulTalk.Modules.Friends.Services.Mocks;
using BeautifulTalk.Modules.Friends.Views;
using BeautifulTalkInfrastructure.RegionNames;
using BeautifulTalkInfrastructure.ViewNames;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Friends
{
    public class FriendsModuleInit : IModule
    {
        private IUnityContainer m_Container;
        private IRegionManager m_RegionManager;

        public FriendsModuleInit(IUnityContainer container, IRegionManager regionManager)
        {
            this.m_Container = container;
            this.m_RegionManager = regionManager;
        }

        public void Initialize()
        {
            //Mock
            //this.m_Container.RegisterType<ICollectFriendsService, MockCollectFriendsService>();
            //this.m_Container.RegisterType<ICollectRecommendUsers, MockCollectRecommendUsers>();
            //this.m_Container.RegisterType<IAddFriendService, MockAddFriendService>();

            //Real
            this.m_Container.RegisterType<ICollectFriendsService, CollectFriendsService>();
            this.m_Container.RegisterType<ICollectRecommendUsers, CollectRecommendUsers>();
            this.m_Container.RegisterType<IAddFriendService, AddFriendService>();

            this.m_Container.RegisterType<IGetUserInfoService, GetUserInfoService>();
            this.m_Container.RegisterType<IGetRoomInfoService, GetRoomInfoService>();
            this.m_Container.RegisterType<object, FriendsView>(FriendsViewNames.FriendsView);
            this.m_Container.RegisterType<object, RecommendView>(FriendsViewNames.RecommendView);
            
            this.m_RegionManager.RegisterViewWithRegion(BusinessRegionNames.TabbingRegion, () => this.m_Container.Resolve<FriendsMainView>());
            this.m_RegionManager.RegisterViewWithRegion(FriendsRegionNames.NavigationRegion, () => this.m_Container.Resolve<FriendsView>());
        }
    }
}
