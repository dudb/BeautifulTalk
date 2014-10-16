using BeautifulTalk.Modules.Rooms.Controllers;
using BeautifulTalk.Modules.Rooms.Services;
using BeautifulTalk.Modules.Rooms.Services.Mocks;
using BeautifulTalk.Modules.Rooms.Views;
using BeautifulTalkInfrastructure.Interfaces;
using BeautifulTalkInfrastructure.PubSubEvents;
using BeautifulTalkInfrastructure.RegionNames;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Rooms
{
    public class RoomsModuleInit : IModule
    {
        private IUnityContainer m_Container;
        private IRegionManager m_RegionManager;

        public RoomsModuleInit(IUnityContainer container, IRegionManager regionManager)
        {
            this.m_Container = container;
            this.m_RegionManager = regionManager;
        }
        
        public void Initialize()
        {
            //Mock
            //this.m_Container.RegisterType<ICollectRoomsService, MockCollectRoomsService>();
            //this.m_Container.RegisterType<IRoomMsgAnalyzable, MockRoomMsgAnalyzer>();
            //this.m_Container.RegisterType<IRoomMsgListener, MockRoomMsgListener>();

            //Real
            this.m_Container.RegisterType<ICollectRoomsService, CollectRoomsService>();
            this.m_Container.RegisterType<IRoomMsgAnalyzable, DevRoomMsgAnalyzer>();
            this.m_Container.RegisterType<IRoomMsgListener, DevRoomMsgListener>();

            this.m_Container.RegisterType<IRoomsControlable, RoomsController>();
            this.m_RegionManager.RegisterViewWithRegion(BusinessRegionNames.TabbingRegion, () => this.m_Container.Resolve<RoomsView>());
        }
    }
}
