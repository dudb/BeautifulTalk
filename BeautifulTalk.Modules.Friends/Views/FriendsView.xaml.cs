using BeautifulTalk.Modules.Friends.ViewModels;
using CommonControl.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BeautifulTalk.Modules.Friends.Views
{
    /// <summary>
    /// Interaction logic for FriendsView.xaml
    /// </summary>
    public partial class FriendsView : NavigatableUserControl
    {
        public FriendsView(FriendsViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel; 
        }
    }
}
