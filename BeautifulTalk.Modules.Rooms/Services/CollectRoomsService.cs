using BeautifulDB.Entities;
using BeautifulDB.Helpers;
using BeautifulTalk.Modules.Rooms.Models;
using BeautifulTalkInfrastructure.Interfaces;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace BeautifulTalk.Modules.Rooms.Services
{
    public class CollectRoomsService : ICollectRoomsService
    {
        public void CollectRooms(RoomCollection rooms, string strMySid)
        {
            var RoomCollection = ConnectionHelper.DB.GetCollection<RoomEntity>("RoomEntity");
            var UserCollection = ConnectionHelper.DB.GetCollection<UserEntity>("UserEntity");
            var FindRoomsQuery = Query<RoomEntity>.EQ(r => r.UserSid, strMySid);
            var FindedRooms = RoomCollection.Find(FindRoomsQuery);

            foreach (RoomEntity r in FindedRooms)
            {
                IList<string> ActiveMemberNickNames = new List<string>();

                foreach (string strUserSid in r.ActiveMemberSids)
                {
                    if (strMySid == strUserSid) continue;
                    var FindUserQuery = Query<UserEntity>.EQ(u => u.Sid, strUserSid);
                    var FindedUser = UserCollection.FindOne(FindUserQuery);
                    if (null != FindedUser) { ActiveMemberNickNames.Add(FindedUser.NickName); }
                }

                rooms.Add(new Room(r.Sid, ActiveMemberNickNames, r.UnReadMsgCount, r.LastMsgSummary, r.LastMsgDate, r.ThumbnailPath, rooms));
            }
        }
    }
}
