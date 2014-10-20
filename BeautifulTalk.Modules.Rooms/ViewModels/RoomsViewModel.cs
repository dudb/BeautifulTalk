using BeautifulDB.Entities;
using BeautifulTalk.Modules.Chatting;
using BeautifulTalk.Modules.Rooms.Controllers;
using BeautifulTalk.Modules.Rooms.Models;
using BeautifulTalk.Modules.Rooms.Services;
using BeautifulTalkInfrastructure.AliveInformation;
using BeautifulTalkInfrastructure.Interfaces;
using BeautifulTalkInfrastructure.Logger;
using BeautifulTalkInfrastructure.PubSubEvents;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace BeautifulTalk.Modules.Rooms.ViewModels
{
    public class RoomsViewModel : BindableBase, IRoomsTabHeaderInfoProvider
    {
        private IEnumerable<Room> m_HeaderNotification;
        private DependencyObject m_TabHeaderImage;
        private DependencyObject m_SelectedTabHeaderImage;
        private ILoggerFacade m_Logger;
        private IUnityContainer m_UnityContainer;
        private IEventAggregator m_EventAggregator;

        private IRoomMsgListener m_RoomMsgListener;
        private IRoomsControlable m_RoomsController;
        private ICollectRoomsService m_CollectRoomService;
        public DelegateCommand<Room> ItemDoubleClickedCommand { get; private set; }
        public RoomCollection Rooms { get; private set; }
        public DependencyObject HeaderContent { get { return m_TabHeaderImage; } }
        public DependencyObject SelectedHeaderContent { get { return m_SelectedTabHeaderImage; } }
        public IEnumerable<Room> HeaderNotification 
        {
            get { return this.m_HeaderNotification; }
            set { SetProperty(ref this.m_HeaderNotification, value); }
        }
        public RoomsViewModel(ILoggerFacade logger, IUnityContainer unityContainer, IEventAggregator eventAggregator)
        {
            if (null == logger) throw new ArgumentNullException("logger");
            if (null == unityContainer) throw new ArgumentNullException("unityContainer");
            if (null == eventAggregator) throw new ArgumentNullException("eventAggregator");
            
            this.m_Logger = logger;
            this.m_UnityContainer = unityContainer;
            this.m_EventAggregator = eventAggregator;
            
            this.HeaderNotification = this.Rooms = new RoomCollection();
            this.m_CollectRoomService = this.m_UnityContainer.Resolve<ICollectRoomsService>();
                  
            this.m_RoomsController = this.m_UnityContainer.Resolve<IRoomsControlable>
                (new ParameterOverride("rooms", this.Rooms),
                    new ParameterOverride("tabHeaderNotification", this));

            this.m_RoomMsgListener = this.m_UnityContainer.Resolve<IRoomMsgListener>
                    (new ParameterOverride("msgAnalyzer", this.m_UnityContainer.Resolve<IRoomMsgAnalyzable>
                        (new ParameterOverride("roomsController", this.m_RoomsController))));

            
            this.ItemDoubleClickedCommand = new DelegateCommand<Room>(ItemDoubleClickRaised);
            this.InitializeHeaderImages();
            this.m_CollectRoomService.CollectRooms(this.Rooms, AuthRepository.MQKeyInfo.UserSid);
            Task.Run(() => this.m_RoomMsgListener.StartListen());
        }
        private void InitializeHeaderImages()
        {
            try
            {
                Image TabHeaderImage = new Image();
                TabHeaderImage.Source = new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/beautifulfreak_chatting.png", UriKind.Relative));
                this.m_TabHeaderImage = TabHeaderImage;

                Image SelectedTabHeaderImage = new Image();
                SelectedTabHeaderImage.Source = new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/beautifulfreak_chatting_selected.png", UriKind.Relative));
                this.m_SelectedTabHeaderImage = SelectedTabHeaderImage;
            }
            catch (UriFormatException uriFormatException)
            {
                m_Logger.Log("Invalid Uri Format for ImageSource inside RoomsModule\n" + uriFormatException.Message, Category.Exception, Priority.Medium);
                throw uriFormatException;
            }
            catch (ArgumentNullException argsNullException)
            {
                m_Logger.Log("UriString is Null inside RoomsModule\n" + argsNullException.Message, Category.Exception, Priority.Medium);
                throw argsNullException;
            }
            catch (ArgumentException argsException)
            {
                m_Logger.Log("UriKind is invalid inside RoomsModule\n" + argsException.Message, Category.Exception, Priority.Medium);
                throw argsException;
            }
        }

        private void ItemDoubleClickRaised(Room targetRoom)
        {
            Task.Run(() => this.m_RoomsController.CreateChattingShellOrActivate(targetRoom.Sid));
        }
    }    
}
