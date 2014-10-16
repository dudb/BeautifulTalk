using BeautifulTalk.ViewModels;
using BeautifulTalkInfrastructure.Interfaces;
using BeautifulTalkInfrastructure.ShellBases;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BeautifulTalk.Views
{
    public partial class LoginShellView : ShellViewBase
    {
        public LoginShellView(LoginShellViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void ShellViewBase_Closed(object sender, EventArgs e)
        {

        }
    }
}
