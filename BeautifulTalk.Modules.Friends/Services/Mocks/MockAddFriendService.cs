using BeautifulTalk.Modules.Friends.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Friends.Services.Mocks
{
    public class MockAddFriendService : IAddFriendService
    {
        public bool AddFriend(string strMySID, string strFriendID)
        {
            return false;
        }
    }
}
