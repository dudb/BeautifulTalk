using BeautifulTalk.Modules.Chatting.Infrastructures;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BeautifulTalk.Modules.Chatting.Converters
{
    public class ToDatetimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == value || null == parameter) return value;

            DatetimeFormats targetFormat = (DatetimeFormats)parameter;
            string strFormattedDatetime = null;

            switch (targetFormat)
            {
                case DatetimeFormats.DateMsg:
                    {
                        strFormattedDatetime = DateTime.FromBinary((long)value).ToLocalTime().ToString("dddd dd MMMM yyyy");
                        break;
                    }
                case DatetimeFormats.ChatMsgTail:
                    {
                        strFormattedDatetime = DateTime.FromBinary((long)value).ToLocalTime().ToString("hh:mm tt");
                        break;
                    }
                default: break;
            }

            return strFormattedDatetime;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
