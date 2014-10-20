using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalkInfrastructure.Interfaces
{
    public interface ITabHeaderNotificationProvider<T>
    {
        T HeaderNotification { get; set; }
    }
}
