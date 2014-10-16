using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BeautifulTalk.Modules.Rooms.Converters
{
    public class RoomMembersNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == value) return value;
            if (typeof(List<string>) != value.GetType()) return value;

            var Members = value as List<string>;
            return string.Join(", ", Members);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
