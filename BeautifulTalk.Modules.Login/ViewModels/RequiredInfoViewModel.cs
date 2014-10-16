using BeautifulTalk.Modules.Login.Models;
using BeautifulTalkInfrastructure.RegionNames;
using BeautifulTalkInfrastructure.ViewNames;
using CommonControl.BusyIndicator;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace BeautifulTalk.Modules.Login.ViewModels
{
    public class RequiredInfoViewModel : BindableBase, INavigationAware
    {
        private RequiredInfoModel m_RequiredInfoModel;
        private ILoggerFacade m_Logger;
        private IRegionManager m_RegionManager;

        public DelegateCommand PreviousCommand { get; private set; }
        public DelegateCommand<object> NextCommand { get; private set; }
        public DelegateCommand<UIElement> InitialFocusCommand { get; private set; }
        public DelegateCommand TextChangedCommand { get; private set; }
        public RequiredInfoModel RequiredInfoModel
        {
            get { return this.m_RequiredInfoModel; }
            set { SetProperty(ref this.m_RequiredInfoModel, value); }
        }
        public RequiredInfoViewModel(ILoggerFacade logger, IRegionManager regionManager)
        {
            this.m_Logger = logger;
            this.m_RegionManager = regionManager;

            this.RequiredInfoModel = new RequiredInfoModel();
            this.PreviousCommand = new DelegateCommand(ExecutePreviousCommand);
            this.NextCommand = new DelegateCommand<object>(ExecuteNextCommand, CanExecuteNextCommand);
            this.InitialFocusCommand = new DelegateCommand<UIElement>(ExecuteInitialFocusCommand);
            this.TextChangedCommand = new DelegateCommand(ExecuteTextChangedCommand);
        }

        private void ExecuteInitialFocusCommand(UIElement targetElement)
        {
            if (null != targetElement) { targetElement.Focus(); }
        }
        private void ExecuteTextChangedCommand()
        {
            this.NextCommand.RaiseCanExecuteChanged();
        }

        private void ExecutePreviousCommand()
        {
            var LoginViewUri = new Uri(LoginViewNames.LoginView, UriKind.Relative);
            this.m_RegionManager.RequestNavigate(LoginRegionNames.LoginRegion, LoginViewUri);
        }

        #region NextCommand
        private void ExecuteNextCommand(object objParam)
        {
            var InterestViewUri = new Uri(LoginViewNames.InterestView, UriKind.Relative);
            this.m_RegionManager.RequestNavigate(LoginRegionNames.LoginRegion, InterestViewUri);
        }

        private bool CanExecuteNextCommand(object objParam)
        {
            if (null == objParam) return false;

            return (bool)objParam;
        }
        #endregion NextCommand

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if (LoginViewNames.RequiredInfoView == navigationContext.Uri.OriginalString) return true;
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            navigationContext.Parameters.Add(this.GetType().GetHashCode().ToString(), this.RequiredInfoModel);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            return;
        }
    }
}
