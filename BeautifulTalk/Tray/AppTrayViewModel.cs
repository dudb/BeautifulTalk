using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BeautifulTalk.Tray
{
    public class AppTrayViewModel
    {
        /// <summary>
        /// Shows a window, if none is already open.
        /// </summary>
        public ICommand ActivateMainWindowCommand
        {
            get
            {
                return new DelegateCommand
                (
                    () =>
                        {
                            Application.Current.MainWindow.Show();
                        },
                    () => true
                );
            }
        }

        /// <summary>
        /// Hides the main window. This command is only enabled if a window is open.
        /// </summary>
        public ICommand HideWindowCommand
        {
            get
            {
                return new DelegateCommand
                (
                    () => Application.Current.MainWindow.Hide(), 
                    () => Application.Current.MainWindow != null
                );
            }
        }

        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand
                (
                    () => Environment.Exit(0)
                );
            }
        }
    }
}
