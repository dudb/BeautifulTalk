using BeautifulTalk.Modules.Rooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Rooms.Services
{
    public interface ICollectRoomsService
    {
        void CollectRooms(RoomCollection rooms, string strMySid);
    }
}
