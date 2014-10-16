using BeautifulDB.Entities;
using BeautifulDB.Helpers;
using BeautifulTalkInfrastructure.Interfaces;
using BeautifulTalkInfrastructure.ShellBases;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Chatting.ViewModels
{
    public class ChattingShellViewModel : MinMaxClosableShellViewModelBase
    {
        public string RoomSID { get; private set; }
        public string ShellTitle { get; private set; }
        public string StateMessage { get; private set; }
        public DelegateCommand WindowClosedCommand { get; private set; }
        public DelegateCommand ActivatedCommand { get; private set; }
        public IRoomsControlable RoomsController { get; private set; }
        public ChattingShellViewModel(string strRoomSID, string strShellTitle, string strStateMessage, IRoomsControlable roomsContoller)
        {
            if (null == strRoomSID) throw new ArgumentNullException("strRoomSID");
            if (null == strShellTitle) throw new ArgumentNullException("strShellTitle");
            if (null == strStateMessage) throw new ArgumentNullException("strStateMessage");
            if (null == roomsContoller) throw new ArgumentNullException("roomsContoller");

            this.RoomSID = strRoomSID;
            this.ShellTitle = strShellTitle;
            this.StateMessage = strStateMessage;
            this.RoomsController = roomsContoller;

            this.WindowClosedCommand = new DelegateCommand(ExecuteWindowClosedCommand);
            this.ActivatedCommand = new DelegateCommand(ExecuteActivatedCommand);
        }
        private void ExecuteWindowClosedCommand()
        {
            this.RoomsController.RemoveChattingShell(this.RoomSID);
        }
        private void ExecuteActivatedCommand()
        {
            this.RoomsController.ResetUnReadCountForRoom(this.RoomSID);
        }

        public override void CloseWindow(IWindowBehavior window)
        {
            if (null == this.RoomSID) throw new NullReferenceException("RoomSID");
            if (null == this.RoomsController) throw new NullReferenceException("RoomsController");

            base.CloseWindow(window);
        }
    }
}
