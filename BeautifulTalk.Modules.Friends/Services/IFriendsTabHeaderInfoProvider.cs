using BeautifulTalk.Modules.Friends.Models;
using BeautifulTalkInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BeautifulTalk.Modules.Friends.Services
{
    public interface IFriendsTabHeaderInfoProvider : ITabHeaderContentProvider<DependencyObject>, ITabHeaderNotificationProvider<IEnumerable<Friend>>
    {
    }
}
