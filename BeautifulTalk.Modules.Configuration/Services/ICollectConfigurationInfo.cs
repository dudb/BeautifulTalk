using BeautifulTalk.Modules.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Configuration.Services
{
    public interface ICollectConfigurationInfo
    {
        void Collect(ConfigurationCategoryCollection categories);
    }
}
