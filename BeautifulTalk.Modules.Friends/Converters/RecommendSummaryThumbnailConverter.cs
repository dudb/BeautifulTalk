using CommonUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BeautifulTalk.Modules.Friends.Converters
{
    public class RecommendSummaryThumbnailConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == value)
            {
                return ToBitmapImgConverter.DecodeImage(ToBitmapImgConverter.LoadImage
                    (@"pack://application:,,,/BeautifulTalk.Modules.Friends;component/Resources/Images/base_recommenduser.png"));
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
