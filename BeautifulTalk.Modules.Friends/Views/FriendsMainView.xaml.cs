using BeautifulTalk.Modules.Friends.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeautifulTalk.Modules.Friends.Views
{
    /// <summary>
    /// Interaction logic for FriendsMainView.xaml
    /// </summary>
    public partial class FriendsMainView : UserControl
    {
        public FriendsMainView(FriendsMainViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
