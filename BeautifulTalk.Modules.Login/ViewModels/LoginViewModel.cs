using BeautifulTalk.Modules.Login.Models;
using BeautifulTalk.Modules.Login.Services;
using BeautifulTalk.Modules.Login.Services.Client;
using BeautifulTalkInfrastructure.Logger;
using BeautifulTalkInfrastructure.PubSubEvents;
using BeautifulTalkInfrastructure.RegionNames;
using BeautifulTalkInfrastructure.Validator;
using BeautifulTalkInfrastructure.ViewNames;
using CommonControl.BusyIndicator;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BeautifulTalk.Modules.Login.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        /// <summary>
        /// Try Login with three Parameters(Id, Password and IsAutoLogin Properties).
        /// </summary>
        public DelegateCommand<UIElement> LoginCommand { get; private set; }

        /// <summary>
        /// Whenever characters is changing on LoginCombobox and PwdTextbox, it occurs to re-evaluate CanExecuteLogin Method.
        /// </summary>
        public DelegateCommand TextChangedCommand { get; private set; }

        /// <summary>
        /// Navigate to special URI to guide about client Account problem.
        /// </summary>
        public DelegateCommand<Uri> NavigateToCommand { get; private set; }

        /// <summary>
        /// Focus on targetElement which is PasswordBox after loading about LoginView.
        /// </summary>
        public DelegateCommand<UIElement> InitialFocusCommand { get; private set; }
        public DelegateCommand<SmoothBusyIndicator> InitialLoadedCommand { get; private set; }
        public DelegateCommand NavigateRequiredInfoViewCommand { get; private set; }

        private LoginModel m_LoginModel;
        private ILoggerFacade m_Logger;
        private ILoginTriableService m_LoginRequester;
        private IUnityContainer m_UnityContainer;
        private IEventAggregator m_EventAggregator;
        private IRegionManager m_RegionManager;
        private SmoothBusyIndicator m_BusyIndicator;
        private Identifications m_ConnectedIdentifications;

        public LoginModel LoginModel
        {
            get { return this.m_LoginModel; }
            set { SetProperty(ref this.m_LoginModel, value); }
        }
        public Identifications ConnectedIdentifications
        {
            get { return this.m_ConnectedIdentifications; }
            set { SetProperty(ref this.m_ConnectedIdentifications, value); }
        }

        public LoginViewModel() { }

        public LoginViewModel(ILoggerFacade logger, IUnityContainer unityContainer, IRegionManager regionManager,
            IEventAggregator eventAggregator, ITrackSuccessConnectionService trackService)
        {
            if (null == logger) throw new ArgumentNullException("logger");
            if (null == unityContainer) throw new ArgumentNullException("unityContainer");
            if (null == regionManager) throw new ArgumentNullException("regionManager");
            if (null == eventAggregator) throw new ArgumentNullException("eventAggregator");
            if (null == trackService) throw new ArgumentNullException("trackService");
            
            this.m_Logger = logger;
            this.m_UnityContainer = unityContainer;
            this.m_RegionManager = regionManager;
            this.m_EventAggregator = eventAggregator;
            
            this.LoginModel = new LoginModel();
            this.LoginCommand = new DelegateCommand<UIElement>(ExecuteLogin, CanExecuteLogin);
            this.TextChangedCommand = new DelegateCommand(ExecuteTextChanged);
            this.NavigateToCommand = new DelegateCommand<Uri>(ExecuteNavigateTo);
            this.InitialFocusCommand = new DelegateCommand<UIElement>(ExecuteInitialFocusCommand);
            this.InitialLoadedCommand = new DelegateCommand<SmoothBusyIndicator>(ExecuteInitialLoadedCommand);
            this.NavigateRequiredInfoViewCommand = new DelegateCommand(ExecuteNavigateRequiredInfoViewCommand);

            this.ConnectedIdentifications = trackService.GetListOfSuccessConnections();
        }
        private void ExecuteInitialLoadedCommand(SmoothBusyIndicator busyIndicator)
        {
            this.m_BusyIndicator = busyIndicator;
        }
        private void ExecuteNavigateRequiredInfoViewCommand()
        {
            var RequiredInfoViewUri = new Uri(LoginViewNames.RequiredInfoView, UriKind.Relative);
            this.m_RegionManager.RequestNavigate(LoginRegionNames.LoginRegion, RequiredInfoViewUri);
        }
        private void ExecuteInitialFocusCommand(UIElement targetElement)
        {
            this.m_Logger.Log("FocusOn targetEelement Raised", Category.Info, Priority.None);
            targetElement.Focus();
        }

        private void ExecuteTextChanged()
        {
            this.m_Logger.Log("TextChanged on IdBox or PasswordBox Raised", Category.Info, Priority.None);
            this.LoginCommand.RaiseCanExecuteChanged();
        }

        private bool CanExecuteLogin(UIElement loginButton)
        {
            this.m_Logger.Log("CanExecuteLogin Raised", Category.Info, Priority.None);

            bool bValidateID = IDvalidator.Validate(this.m_LoginModel.Id);
            bool bValidatePwd = PasswordValidator.Validate(this.m_LoginModel.Password);
            bool bIsBusy = true;
            if (null != this.m_BusyIndicator) { bIsBusy = this.m_BusyIndicator.IsBusy; }

            return (bValidateID && bValidatePwd && !bIsBusy);
        }

        /// <summary>
        /// StartPoint to request login
        /// </summary>
        private void ExecuteLogin(UIElement loginButton)
        {
            this.m_Logger.Log("ExecuteLogin Raised", Category.Info, Priority.None);

            if (null == m_LoginRequester) { m_LoginRequester = this.m_UnityContainer.Resolve<LoginRequestService>(); }
            
            Task.Run(() => m_LoginRequester.TryLogin(this.LoginCommand, this.m_BusyIndicator, loginButton, m_LoginModel))
                .ContinueWith(t => 
                {
                    if (true == t.Result) { this.m_EventAggregator.GetEvent<StartupBusinessShellEvent>().Publish(null); }
                });
        }

        private void ExecuteNavigateTo(Uri uriForNavigate)
        {
            this.m_Logger.Log("NavigateTo uri Raised", Category.Info, Priority.None);
            Process.Start(new ProcessStartInfo(uriForNavigate.AbsoluteUri));
        }
    }
}
