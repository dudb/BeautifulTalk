using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalkInfrastructure.Validator
{
    public class PasswordValidator
    {
        public static bool Validate(string strPassword)
        {
            if (string.IsNullOrEmpty(strPassword)) return false;
            return true;
        }
    }
}
