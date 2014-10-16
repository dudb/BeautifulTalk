using BeautifulTalk.ViewModels;
using BeautifulTalkInfrastructure.Interfaces;
using BeautifulTalkInfrastructure.ShellBases;
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
    /// <summary>
    /// Interaction logic for BusinessShellView.xaml
    /// </summary>
    public partial class BusinessShellView : ShellViewBase
    {
        public BusinessShellView(BusinessShellViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
