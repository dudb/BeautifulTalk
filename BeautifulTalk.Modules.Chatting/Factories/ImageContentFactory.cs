using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BeautifulTalk.Modules.Chatting.Factories
{
    public class ImageContentFactory : ContentFactory, ICreateMsgContent
    {
        public ImageContentFactory(string strContent) : base(strContent) { }
        public DependencyObject Create()
        {
            Image ImageContent = new Image();
            return ImageContent;
        }
    }
}
