using BeautifulTalk.Modules.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BeautifulTalk.Modules.Configuration.Converters
{
    public class CategoryTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null != value && null != parameter)
            {
                var nNewValue = (ConfigurationCategoryType)value;
                var nSource = (ConfigurationCategoryType)parameter;

                if (nNewValue == nSource) { return Visibility.Visible; }
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
