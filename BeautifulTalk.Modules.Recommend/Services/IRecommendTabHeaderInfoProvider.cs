using BeautifulTalkInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BeautifulTalk.Modules.Recommend.Services
{
    public interface IRecommendTabHeaderInfoProvider : ITabHeaderContentProvider<DependencyObject>, IHeaderNotificationProvider<Int32>
    {
    }
}
