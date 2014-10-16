using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BeautifulTalk.Modules.Chatting.Factories
{
    public class TextblockContentFactory : ContentFactory, ICreateMsgContent
    {
        public TextblockContentFactory(string strContent) : base(strContent) { }
        public DependencyObject Create()
        {
            TextBlock TextBlockContent = new TextBlock();
            TextBlockContent.TextWrapping = TextWrapping.Wrap;
            TextBlockContent.Text = this.m_strContent;
            return TextBlockContent;
        }
    }
}
