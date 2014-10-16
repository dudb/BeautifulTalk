using BeautifulTalk.Modules.Chatting.Factories;
using BeautifulTalkInfrastructure.ProtocolFormat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace BeautifulTalk.Modules.Chatting.Converters
{
    public class FilteringMsgConverter : IMultiValueConverter
    {
        private ICreateMsgContent m_ContentFactory;
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == values) throw new ArgumentNullException("values");
            if (2 != values.Length) throw new IndexOutOfRangeException("values");
            if (typeof(ContentType) != values[0].GetType()) throw new InvalidDataException("values[0]");
            if (typeof(string) != values[1].GetType()) throw new InvalidDataException("values[1]");

            ContentType nContentType = (ContentType)values[0];
            string strContent = values[1].ToString();

            switch (nContentType)
            {
                case ContentType.Text: { this.m_ContentFactory = new TextblockContentFactory(strContent); break; }
                case ContentType.Image: { this.m_ContentFactory = new ImageContentFactory(strContent); break; }
                default: break;
            }

            return this.m_ContentFactory.Create();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
