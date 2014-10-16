using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Rooms.Controllers
{
    public interface IRegistrable<K,V>
    {
        bool Register(K key, V target);
    }
}
