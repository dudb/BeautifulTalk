using BeautifulTalk.Modules.Friends.Services;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BeautifulTalk.Modules.Friends.ViewModels
{
    public class FriendsMainViewModel : BindableBase, IFriendsTabHeaderInfoProvider
    {
        private readonly ILoggerFacade m_Logger;
        private DependencyObject m_TabHeaderImage;
        private DependencyObject m_SelectedTabHeaderImage;

        public DependencyObject HeaderContent { get { return m_TabHeaderImage; } }
        public DependencyObject SelectedHeaderContent { get { return m_SelectedTabHeaderImage; } }

        public int HeaderNotification
        {
            get;
            set;
        }

        public FriendsMainViewModel(ILoggerFacade logger)
        {
            if (null == logger) throw new ArgumentNullException("logger");

            this.m_Logger = logger;
            this.InitializeHeaderImages();
        }

        private void InitializeHeaderImages()
        {
            try
            {
                Image TabHeaderImage = new Image();
                TabHeaderImage.Source = new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Friends;component/Resources/Images/beautifulfreak_friends.png", UriKind.Relative));
                this.m_TabHeaderImage = TabHeaderImage;

                Image SelectedTabHeaderImage = new Image();
                SelectedTabHeaderImage.Source = new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Friends;component/Resources/Images/beautifulfreak_friends_selected.png", UriKind.Relative));
                this.m_SelectedTabHeaderImage = SelectedTabHeaderImage;
            }
            catch (UriFormatException uriFormatException)
            {
                m_Logger.Log("Invalid Uri Format for ImageSource inside FriendsModule\n" + uriFormatException.Message, Category.Exception, Priority.Medium);
                throw uriFormatException;
            }
            catch (ArgumentNullException argsNullException)
            {
                m_Logger.Log("UriString is Null inside FriendsModule\n" + argsNullException.Message, Category.Exception, Priority.Medium);
                throw argsNullException;
            }
            catch (ArgumentException argsException)
            {
                m_Logger.Log("UriKind is invalid inside FriendsModule\n" + argsException.Message, Category.Exception, Priority.Medium);
                throw argsException;
            }
        }
    }
}
