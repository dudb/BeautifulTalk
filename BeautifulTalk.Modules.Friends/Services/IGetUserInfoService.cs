using BeautifulDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Friends.Services
{
    public interface IGetUserInfoService
    {
        UserEntity GetUserInfo(string strUserSID);
    }
}
