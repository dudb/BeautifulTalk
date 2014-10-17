using BeautifulTalk.Modules.Chatting.ViewModels;
using BeautifulTalkInfrastructure.Interfaces;
using BeautifulTalkInfrastructure.ShellBases;
using Microsoft.Practices.Prism.Commands;
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

namespace BeautifulTalk.Modules.Chatting
{
    /// <summary>
    /// Interaction logic for ChattingShellView.xaml
    /// </summary>
    public partial class ChattingShellView : ShellViewBase, IChattingShellView
    {
        public IChattingViewModel ChattingViewModel { get; set; }
        public ChattingShellView(ChattingShellViewModel viewModel)
        {

            InitializeComponent();
            this.DataContext = viewModel;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            this.ChattingViewModel.RequestReadMsgs();
        }
        public bool IsActiveChattingShellView()
        {
            return base.IsActive;
        }

        public void ActivateChattingShellView()
        {
            base.Activate();
        }
        public void ShowChattingShellView()
        {
            base.Show();
        }

        public bool IsFocusedChattingShellView()
        {
            return base.IsFocused;
        }
    }
}
