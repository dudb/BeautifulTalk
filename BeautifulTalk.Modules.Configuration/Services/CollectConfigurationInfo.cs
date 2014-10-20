using BeautifulTalk.Modules.Configuration.Models;
using CommonUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BeautifulTalk.Modules.Configuration.Services
{
    public class CollectConfigurationInfo : ICollectConfigurationInfo
    {
        public void Collect(ConfigurationCategoryCollection categories)
        {
            BitmapImage EditImg = new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Configuration;component/Resources/Images/beautifultalkfreak_edituser.png", UriKind.Relative));
            categories.Add(new ConfigurationCategory(EditImg, "Profile",
                "Edit your information.", ""));

            BitmapImage NotificationImg = new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Configuration;component/Resources/Images/beautifultalkfreak_notification.png", UriKind.Relative));
            categories.Add(new ConfigurationCategory(NotificationImg, "Notification",
                "Check new notices.", ""));

            BitmapImage SettingsImg = new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Configuration;component/Resources/Images/beautifultalkfreak_settings.png", UriKind.Relative));
            categories.Add(new ConfigurationCategory(SettingsImg, "Settings",
                "Make your environment.", ""));
        }
    }
}
