using BeautifulDB.Entities;
using BeautifulDB.Helpers;
using BeautifulTalkInfrastructure.AliveInformation;
using BeautifulTalkInfrastructure.Interfaces;
using BeautifulTalkInfrastructure.Logger;
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
        public ChattingShellViewModel(string strRoomSID, IRoomsControlable roomsContoller)
        {
            if (null == strRoomSID) throw new ArgumentNullException("strRoomSID");
            if (null == roomsContoller) throw new ArgumentNullException("roomsContoller");

            this.RoomSID = strRoomSID;
            this.RoomsController = roomsContoller;

            this.WindowClosedCommand = new DelegateCommand(ExecuteWindowClosedCommand);
            this.ActivatedCommand = new DelegateCommand(ExecuteActivatedCommand);
            this.InitializeShellTitleAndMessage(strRoomSID);
        }

        private void InitializeShellTitleAndMessage(string strRoomSid)
        {
            var RoomCollection = ConnectionHelper.DB.GetCollection<RoomEntity>("RoomEntity");
            var FindRoomQuery = Query<RoomEntity>.EQ(r => r.Sid, strRoomSid);
            var FindedRoomEntity = RoomCollection.FindOne(FindRoomQuery);

            if (null != FindedRoomEntity)
            { 
                try
                {
                    IList<string> arActiveMemberSids = FindedRoomEntity.ActiveMemberSids;
                    arActiveMemberSids.Remove(AuthRepository.MQKeyInfo.UserSid);
                    IList<string> arActiveMemberNickNames = new List<string>();
                    int nCountOfMembers = FindedRoomEntity.MemberSids.Count - 1;
                    var UserCollection = ConnectionHelper.DB.GetCollection<UserEntity>("UserEntity");
                    string strComment = string.Empty;

                    foreach (string strMemberSid in arActiveMemberSids)
                    {
                        var FindUserQuery = Query<UserEntity>.EQ(u => u.Sid, strMemberSid);
                        var FindedUser = UserCollection.FindOne(FindUserQuery);

                        if (null != FindedUser)
                        {
                            arActiveMemberNickNames.Add(FindedUser.NickName);
                            strComment = FindedUser.Comment;
                        }
                    }

                    this.ShellTitle = string.Join(", ", arActiveMemberNickNames);

                    if (1 == nCountOfMembers)
                    {
                        this.StateMessage = strComment;
                    }
                    else
                    {
                        this.StateMessage = arActiveMemberSids.Count.ToString();
                    }
                }
                catch(Exception unExpectedException)
                {
                    GlobalLogger.Log(unExpectedException.Message);
                }
            }
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
