using BeautifulTalk.Modules.Login.Models;
using CommonControl.BusyIndicator;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BeautifulTalk.Modules.Login.Services.Client
{
    public interface ILoginTriableService
    {
        bool TryLogin(DelegateCommand<UIElement> loginCommand, SmoothBusyIndicator busyIndicator, UIElement loginButton, LoginModel loginModel);
    }
}
