using BeautifulTalk.Modules.Chatting;
using BeautifulTalk.Modules.Rooms;
using BeautifulTalk.Modules.Configuration;
using BeautifulTalk.Modules.Login;
using BeautifulTalk.Views;
using BeautifulTalkInfrastructure.Logger;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BeautifulTalk.Modules.Friends;
using BeautifulTalk.Modules.DetailInfo;

namespace BeautifulTalk
{
    public class TalkBootstrapper : UnityBootstrapper
    {
        protected override ILoggerFacade CreateLogger()
        {
            return TimeBasedLoggerAdapter.CreateLogger();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog ModCatalog = (ModuleCatalog)this.ModuleCatalog;

            ModCatalog.AddModule(typeof(RealTimeInformationCenter));
            ModCatalog.AddModule(typeof(LoginModuleInit));

            Type ChattingListModuleType = typeof(RoomsModuleInit);
            this.ModuleCatalog.AddModule(new ModuleInfo()
                {
                    ModuleName = ChattingListModuleType.Name,
                    ModuleType = ChattingListModuleType.AssemblyQualifiedName,
                    InitializationMode = InitializationMode.OnDemand
                });

            Type FriendsModuleType = typeof(FriendsModuleInit);
            this.ModuleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = FriendsModuleType.Name,
                ModuleType = FriendsModuleType.AssemblyQualifiedName,
                InitializationMode = InitializationMode.OnDemand
            });

            Type ConfigurationModuleType = typeof(ConfigurationModuleInit);
            this.ModuleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = ConfigurationModuleType.Name,
                ModuleType = ConfigurationModuleType.AssemblyQualifiedName,
                InitializationMode = InitializationMode.OnDemand
            });

            Type DetailInfoModuleType = typeof(DetailInfoModuleInit);
            this.ModuleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = DetailInfoModuleType.Name,
                ModuleType = DetailInfoModuleType.AssemblyQualifiedName,
                InitializationMode = InitializationMode.OnDemand
            });
        }
        
        protected override DependencyObject CreateShell()
        {
            LoginShellView LogInShellView = this.Container.TryResolve<LoginShellView>();

            return LogInShellView;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }
    }
}
