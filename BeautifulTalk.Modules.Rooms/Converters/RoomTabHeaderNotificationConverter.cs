using BeautifulTalk.Modules.Rooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BeautifulTalk.Modules.Rooms.Converters
{
    public class RoomTabHeaderNotificationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var Rooms = value as IEnumerable<Room>;

            if (null == Rooms) return 0;
            else
            {
                return Rooms.Sum(r => r.UnReadMsgCount);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
