using BeautifulDB.Entities;
using BeautifulDB.Helpers;
using BeautifulTalk.Modules.Friends.Models;
using BeautifulTalkInfrastructure.Logger;
using BeautifulTalkInfrastructure.ProtocolFormat;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace BeautifulTalk.Modules.Friends.Services
{
    public class CollectFriendsService : ICollectFriendsService
    {
        public void CollectFriends(string strUserSID, FriendCollection friends)
        {
            try
            {
                var FriendsCollection = ConnectionHelper.DB.GetCollection<FriendsEntity>("FriendsEntity");
                var FindFriendsQuery = Query<FriendsEntity>.EQ(f => f.UserSID, strUserSID);
                var FindedFriends = FriendsCollection.FindOne(FindFriendsQuery);

                if (null != FindedFriends)
                {
                    var UserCollection = ConnectionHelper.DB.GetCollection<UserEntity>("UserEntity");

                    Parallel.ForEach(FindedFriends.FriendSIDs, fSid =>
                    {
                        var FindUserQuery = Query<UserEntity>.EQ(u => u.Sid, fSid);
                        var FindedUser = UserCollection.FindOne(FindUserQuery);

                        if (null != FindedUser)
                        {
                            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new ThreadStart(() =>
                            {
                                friends.Add(new Friend(FindedUser.Thumbnail, FindedUser.UserId, FindedUser.Sid, FindedUser.NickName, FindedUser.Comment));
                            }));
                        }
                    });
                }
            }
            catch (Exception unExpectedException)
            {
                GlobalLogger.Log(unExpectedException.Message);
            }
        }
    }
}
