using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace BeautifulTalk.Modules.Chatting.Converters
{
    public class ToBitmapSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == value ||typeof(byte[]) != value.GetType())
            {
                return new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/1.png", UriKind.Relative));
            }

            MemoryStream ms = new MemoryStream((byte[])value);
            ms.Seek(0, SeekOrigin.Begin);

            BitmapImage BitmapImg = new BitmapImage();
            BitmapImg.BeginInit();
            BitmapImg.DecodePixelHeight = 100;
            BitmapImg.StreamSource = ms;
            BitmapImg.EndInit();

            return BitmapImg;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
