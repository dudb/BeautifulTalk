using BeautifulTalkInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BeautifulTalk.Modules.Configuration.Services
{
    public interface IConfigurationTabHeaderInfoProvider : ITabHeaderContentProvider<DependencyObject>, ITabHeaderNotificationProvider<Int32>
    {
    }
}
