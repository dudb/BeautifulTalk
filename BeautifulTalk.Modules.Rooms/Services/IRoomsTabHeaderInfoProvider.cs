using BeautifulTalk.Modules.Rooms.Models;
using BeautifulTalkInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BeautifulTalk.Modules.Rooms.Services
{
    public interface IRoomsTabHeaderInfoProvider : ITabHeaderContentProvider<DependencyObject>, ITabHeaderNotificationProvider<IEnumerable<Room>>
    {
    }
}
