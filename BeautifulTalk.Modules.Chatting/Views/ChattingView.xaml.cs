using BeautifulTalk.Modules.Chatting.ViewModels;
using BeautifulTalkInfrastructure.Interfaces;
using Microsoft.Practices.Unity;
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

namespace BeautifulTalk.Modules.Chatting.Views
{
    /// <summary>
    /// Interaction logic for ChattingView.xaml
    /// </summary>
    public partial class ChattingView : UserControl, IChattingViewBehavior
    {
        public ChattingView(ChattingViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        
        public void ScrollIntoView(object targetObject)
        {
            this.MsgListBox.ScrollIntoView(targetObject);
        }

        public void ClearInput()
        {
            this.InputBox.ClearInput();
        }
    }
}
