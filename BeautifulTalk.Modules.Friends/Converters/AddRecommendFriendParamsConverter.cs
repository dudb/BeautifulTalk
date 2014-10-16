using BeautifulTalkInfrastructure.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BeautifulTalk.Modules.Friends.Converters
{
    public class AddRecommendFriendParams
    {
        public UIElement Source{ get; set; }
        public string RecommendUserSid { get; set; }
    }
    public class AddRecommendFriendParamsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var Params = new AddRecommendFriendParams();

            try
            {
                Params.Source = values[0] as UIElement;
                Params.RecommendUserSid = values[1].ToString();
            }
            catch (Exception unExpectedException)
            {
                GlobalLogger.Log(unExpectedException.Message);
            }

            return Params;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
