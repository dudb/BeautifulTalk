using BeautifulDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Friends.Services
{
    public interface IGetRoomInfoService
    {
        RoomEntity GetRoomInfo(IList<string> arMembers);
    }
}
