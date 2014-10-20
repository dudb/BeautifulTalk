using BeautifulTalk.Modules.Configuration.Models;
using BeautifulTalk.Modules.Configuration.Services;
using BeautifulTalkInfrastructure.Interfaces;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BeautifulTalk.Modules.Configuration.ViewModels
{
    public class ConfigurationViewModel : BindableBase, IConfigurationTabHeaderInfoProvider
    {
        private DependencyObject m_TabHeaderImage;
        private DependencyObject m_SelectedTabHeaderImage;
        private ILoggerFacade m_Logger;
        private IUnityContainer m_UnityContainer;
        private IEventAggregator m_EventAggregator;
        private ICollectConfigurationInfo m_CollectConfigurationInfo;
        public DependencyObject HeaderContent { get { return m_TabHeaderImage; } }
        public DependencyObject SelectedHeaderContent { get { return m_SelectedTabHeaderImage; } }
        public int HeaderNotification
        {
            get;
            set;
        }
        public ConfigurationCategoryCollection ConfigurationCategories { get; private set; }
        public ConfigurationViewModel(ILoggerFacade logger, IUnityContainer unityContainer, IEventAggregator eventAggregator,
            ICollectConfigurationInfo collectConfigurationInfo)
        {
            if (null == logger) throw new ArgumentNullException("logger");
            if (null == unityContainer) throw new ArgumentNullException("unityContainer");
            if (null == eventAggregator) throw new ArgumentNullException("eventAggregator");
            if (null == collectConfigurationInfo) throw new ArgumentNullException("collectConfigurationInfo");

            this.m_Logger = logger;
            this.m_UnityContainer = unityContainer;
            this.m_EventAggregator = eventAggregator;
            this.m_CollectConfigurationInfo = collectConfigurationInfo;

            this.ConfigurationCategories = new ConfigurationCategoryCollection();
            collectConfigurationInfo.Collect(this.ConfigurationCategories);
            
            this.InitializeHeaderImages();
        }
        private void InitializeHeaderImages()
        {
            try
            {
                Image TabHeaderImage = new Image();
                TabHeaderImage.Source = new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Configuration;component/Resources/Images/beautifulfreak_configuration.png", UriKind.Relative));
                this.m_TabHeaderImage = TabHeaderImage;

                Image SelectedTabHeaderImage = new Image();
                SelectedTabHeaderImage.Source = new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Configuration;component/Resources/Images/beautifulfreak_configuration_selected.png", UriKind.Relative));
                this.m_SelectedTabHeaderImage = SelectedTabHeaderImage;
            }
            catch (UriFormatException uriFormatException)
            {
                m_Logger.Log("Invalid Uri Format for ImageSource inside RecommendModule\n" + uriFormatException.Message, Category.Exception, Priority.Medium);
                throw uriFormatException;
            }
            catch (ArgumentNullException argsNullException)
            {
                m_Logger.Log("UriString is Null inside RecommendModule\n" + argsNullException.Message, Category.Exception, Priority.Medium);
                throw argsNullException;
            }
            catch (ArgumentException argsException)
            {
                m_Logger.Log("UriKind is invalid inside RecommendModule\n" + argsException.Message, Category.Exception, Priority.Medium);
                throw argsException;
            }
        }
    }
}
