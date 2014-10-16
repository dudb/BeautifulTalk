using CommonUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BeautifulTalk.Modules.Recommend.Converters
{
    public class FriendThumbnailConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == value)
            {
                return ToBitmapImgConverter.DecodeImage(ToBitmapImgConverter.LoadImage
                    (@"pack://application:,,,/BeautifulTalk.Modules.Recommend;component/Resources/Images/base_friend.png"));
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
