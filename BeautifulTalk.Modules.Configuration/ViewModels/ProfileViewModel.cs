using BeautifulTalkInfrastructure.AliveInformation;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Configuration.ViewModels
{
    public class ProfileViewModel : BindableBase
    {
        public MQKeySet ProfileModel { get; private set; }
        public ProfileViewModel()
        {
            this.ProfileModel = AuthRepository.MQKeyInfo;
        }
    }
}
