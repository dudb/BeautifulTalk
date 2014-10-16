using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BeautifulTalk.Modules.Rooms.Converters
{
    public class RoomLastMsgDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return 0;
            if (typeof(long) != value.GetType()) return 0;

            return DateTime.FromBinary((long)value).ToLocalTime().ToShortTimeString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
