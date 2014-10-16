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
        private IDictionary<string, byte[]> m_ThumbnailDictionary;
        private ITabHeaderNotificationProvider<Int32> m_TabHeaderNotification;
        public CollectRoomsService(IDictionary<string, byte[]> thumbnailDictionary, ITabHeaderNotificationProvider<Int32> tabHeaderNotification)
        {
            if (null == thumbnailDictionary) throw new ArgumentNullException("thumbnailDictionary");
            if (null == tabHeaderNotification) throw new ArgumentNullException("tabHeaderNotification");

            this.m_ThumbnailDictionary = thumbnailDictionary;
            this.m_TabHeaderNotification = tabHeaderNotification;
        }
        public void CollectRooms(RoomCollection rooms, string strMySid)
        {
            var RoomCollection = ConnectionHelper.DB.GetCollection<RoomEntity>("RoomEntity");
            var UserCollection = ConnectionHelper.DB.GetCollection<UserEntity>("UserEntity");
            var FindRoomsQuery = Query<RoomEntity>.EQ(r => r.UserSid, strMySid);
            var FindedRooms = RoomCollection.Find(FindRoomsQuery);

            Parallel.ForEach(FindedRooms, r =>
            {
                IList<string> ActiveMemberNickNames = new List<string>();

                foreach (string strUserSid in r.ActiveMemberSids)
                {
                    if (strMySid == strUserSid) continue;
                    var FindUserQuery = Query<UserEntity>.EQ(u => u.Sid, strUserSid);
                    var FindedUser = UserCollection.FindOne(FindUserQuery);
                    if (null != FindedUser) { ActiveMemberNickNames.Add(FindedUser.NickName); }
                }

                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new ThreadStart(() =>
                {
                    rooms.Add(new Room(r.Sid, ActiveMemberNickNames, r.UnReadMsgCount, r.LastMsgSummary, r.LastMsgDate, r.ThumbnailPath));
                    this.m_TabHeaderNotification.HeaderNotification += r.UnReadMsgCount;
                }));
            });
        }
    }
}
