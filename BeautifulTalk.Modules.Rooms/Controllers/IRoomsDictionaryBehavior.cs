using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Rooms.Controllers
{
    public interface IRoomsDictionaryBehavior<K,V> : IRegistrable<K,V>, IRemovable<K>
    {
        bool DoesExist(K key);
        V GetValue(K key);
    }
}
