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
            BitmapImage ProfileImg = new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Configuration;component/Resources/Images/beautifultalkfreak_edituser.png", UriKind.Relative));
            categories.Add(new ConfigurationCategory(ProfileImg, ConfigurationCategoryType.Profile, "Profile",
                "Edit your information.", ""));

            BitmapImage NotificationImg = new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Configuration;component/Resources/Images/beautifultalkfreak_notification.png", UriKind.Relative));
            categories.Add(new ConfigurationCategory(NotificationImg, ConfigurationCategoryType.Notification, "Notification",
                "Check new notices.", ""));

            BitmapImage SettingsImg = new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Configuration;component/Resources/Images/beautifultalkfreak_settings.png", UriKind.Relative));
            categories.Add(new ConfigurationCategory(SettingsImg, ConfigurationCategoryType.Setting, "Settings",
                "Make your environment.", ""));
        }
    }
}
