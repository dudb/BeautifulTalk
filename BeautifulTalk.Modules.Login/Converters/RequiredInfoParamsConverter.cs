using BeautifulTalkInfrastructure.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BeautifulTalk.Modules.Login.Converters
{
    public class RequiredInfoParamsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == values) return false;
            object[] objParams = values as object[];

            if (null == objParams) return false;
            if (null == objParams[0] ||
                null == objParams[1] ||
                null == objParams[2]) return false;

            if (typeof(string) != objParams[0].GetType() ||
                typeof(string) != objParams[1].GetType() ||
                typeof(string) != objParams[2].GetType()) return false;


            bool bValidateID = IDvalidator.Validate(objParams[0].ToString());
            bool bValidatePwd = PasswordValidator.Validate(objParams[1].ToString());
            bool bValidateNickName = !string.IsNullOrEmpty(objParams[2].ToString());
         
            return (bValidateID && bValidatePwd && bValidateNickName);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
