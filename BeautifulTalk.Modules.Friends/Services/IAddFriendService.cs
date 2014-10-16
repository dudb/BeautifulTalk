using BeautifulTalk.Modules.Friends.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Friends.Services
{
    public interface IAddFriendService
    {
        bool AddFriend(string strMySID, string strFriendSID);
    }
}
