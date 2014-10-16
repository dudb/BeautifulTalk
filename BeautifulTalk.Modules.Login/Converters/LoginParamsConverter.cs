using BeautifulTalkInfrastructure.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BeautifulTalk.Modules.Login.Converters
{
    public class LoginParamsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == values) return false;
            object[] objParams = values as object[];

            if (null == objParams) return false;
            if (null == objParams[0] ||
                null == objParams[1]) return false;

            if (typeof(string) != objParams[0].GetType() ||
                typeof(string) != objParams[1].GetType()) return false;


            bool bValidateID    = IDvalidator.Validate(objParams[0].ToString());
            bool bValidatePwd   = PasswordValidator.Validate(objParams[1].ToString());

            if (bValidateID && bValidatePwd) return values[2];
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
