using BeautifulDB.Entities;
using BeautifulDB.Helpers;
using BeautifulTalk.Modules.Login.Models;
using BeautifulTalk.Modules.Login.Services;
using BeautifulTalk.Modules.Login.Services.Client;
using BeautifulTalkInfrastructure.ProtocolFormat;
using BeautifulTalkInfrastructure.RegionNames;
using BeautifulTalkInfrastructure.ViewNames;
using CommonControl.BusyIndicator;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace BeautifulTalk.Modules.Login.ViewModels
{
    public class InterestViewModel : BindableBase, INavigationAware
    {
        private const int MIMIMUM_INTERESTS = 5;

        private string m_strErrorMessage;
        private ILoggerFacade m_Logger;
        private IRegionManager m_RegionManager;
        private RequiredInfoModel m_RequiredInfo;
        private InterestCategoryCollection m_Interests;
        private ICollectInterestCategoriesService m_CollectCatagoriesService;
        public string CurrentMessage 
        {
            get { return this.m_strErrorMessage; }
            set { SetProperty(ref m_strErrorMessage, value); }
        }
        public DelegateCommand PreviousCommand { get; private set; }
        public DelegateCommand RegisterCommand { get; private set; }
        public DelegateCommand<UIElement> InitialLoadedCommand { get; private set; }
        public DelegateCommand SelectionChangedCommand { get; private set; }
        public InterestCategoryCollection Categories 
        {
            get { return this.m_Interests; }
            set { SetProperty(ref this.m_Interests, value); }
        }
        public InterestViewModel(ILoggerFacade logger, IRegionManager regionManager, ICollectInterestCategoriesService collectCatagoriesService)
        {
            if (null == logger) throw new ArgumentNullException("logger");
            if (null == regionManager) throw new ArgumentNullException("regionManager");
            if (null == collectCatagoriesService) throw new ArgumentNullException("collectCatagoriesService");

            this.m_Logger = logger;
            this.m_RegionManager = regionManager;
            this.m_CollectCatagoriesService = collectCatagoriesService;

            this.PreviousCommand = new DelegateCommand(ExecutePreviousCommand);
            this.RegisterCommand = new DelegateCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
            this.InitialLoadedCommand = new DelegateCommand<UIElement>(ExecuteInitialLoadedCommand);
            this.SelectionChangedCommand = new DelegateCommand(ExecuteSelectionChangedCommand);
        }

        //If interestView will be loaded, listbox will be focused by this method for follow Register step with Key(enter).
        //and collect interests from the server.
        private void ExecuteInitialLoadedCommand(UIElement targetElement)
        {
            if (null != targetElement) { targetElement.Focus(); }
            
            try
            {
                this.Categories = m_CollectCatagoriesService.CollectCategories();
            }
            catch (FaultException serviceException)
            {
                this.CurrentMessage = "server is unavailable.";
                this.m_Logger.Log(serviceException.Message, Category.Exception, Priority.Medium);
            }
            catch (Exception unExpectedException)
            {
                this.CurrentMessage = "server is unavailable.";
                this.m_Logger.Log(unExpectedException.Message, Category.Exception, Priority.Medium);
            }
        }
        private void ExecuteSelectionChangedCommand()
        {
            this.RegisterCommand.RaiseCanExecuteChanged();
        }
        private void ExecutePreviousCommand()
        {
            var RequiredInfoViewUri = new Uri(LoginViewNames.RequiredInfoView, UriKind.Relative);
            this.m_RegionManager.RequestNavigate(LoginRegionNames.LoginRegion, RequiredInfoViewUri);
        }

        private void ExecuteRegisterCommand()
        {
            var SignUpParameters = this.ExtractRegistrationModel();
            var JsonSignUpParameters = JsonConvert.SerializeObject(SignUpParameters);

            try
            {
                Task.Run(() =>
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.Timeout = TimeSpan.FromSeconds(10);
                        var postdataString = new StringContent(JsonSignUpParameters, new UTF8Encoding(), "application/json");
                        var responseMessage = httpClient.PostAsync(
                            string.Format("{0}{1}", BeautifulTalkProtocolSet.ServerURIwithPort, BeautifulTalkProtocolSet.SignUpURI), postdataString).Result;

                        if (responseMessage.IsSuccessStatusCode)
                        {
                            this.CurrentMessage = "SignUp Success!";

                            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new ThreadStart(() =>
                            {
                                var LoginViewUri = new Uri(LoginViewNames.LoginView, UriKind.Relative);
                                this.m_RegionManager.RequestNavigate(LoginRegionNames.LoginRegion, LoginViewUri);
                            }));
                        }
                        else
                        {
                            this.CurrentMessage = responseMessage.ReasonPhrase;
                        }
                    }
                });
            }
            catch (FaultException serviceException)
            {
                this.CurrentMessage = serviceException.Message;
                this.m_Logger.Log(serviceException.Message, Category.Exception, Priority.Medium);
            }
            catch (Exception unExpectedException)
            {
                this.CurrentMessage = "UnActivating Server...";
                this.m_Logger.Log(unExpectedException.Message, Category.Exception, Priority.Medium);
            }
        }

        private bool CanExecuteRegisterCommand()
        {
            if (null != this.Categories)
            {
                int nCountOfSelectedInterests =
                    this.Categories
                    .Where(i => (true == i.IsSelected))
                    .ToArray().Length;

                if (MIMIMUM_INTERESTS <= nCountOfSelectedInterests) return true;
            }

            return false;
        }

        private JObject ExtractRegistrationModel()
        {
            string[] SelectedCategories =
                this.Categories
                .Where(i => (true == i.IsSelected))
                .Select(i => i.Title).ToArray();

            if (null == SelectedCategories) return null;
            if (null == m_RequiredInfo) return null;
            if (true == string.IsNullOrEmpty(m_RequiredInfo.Id)) return null;
            if (true == string.IsNullOrEmpty(m_RequiredInfo.Password)) return null;

            dynamic Parameters = new JObject();
            Parameters.id = this.m_RequiredInfo.Id;
            Parameters.password = this.m_RequiredInfo.Password;
            Parameters.nickname = this.m_RequiredInfo.NickName;
            Parameters.applicationid = BeautifulTalkProtocolSet.ApplicationId;
            Parameters.interests = new JArray(SelectedCategories);

            return Parameters;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if (LoginViewNames.InterestView == navigationContext.Uri.OriginalString) return true;
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            return;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            m_RequiredInfo = navigationContext.Parameters[typeof(RequiredInfoViewModel).GetHashCode().ToString()] as RequiredInfoModel;
        }
    }
}
