using BeautifulTalk.Modules.Login.Models;
using CommonControl.BusyIndicator;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Login.Services.Client
{
    public interface ILoginTriableService
    {
        bool TryLogin(DelegateCommand<SmoothBusyIndicator> loginCommand, SmoothBusyIndicator busyIndicator, LoginModel loginModel);
    }
}
