using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BeautifulTalkInfrastructure.Validator
{
    public class IDvalidator
    {
        private static bool m_bIsInvalid;
        public static bool Validate(string strID)
        {
            m_bIsInvalid = false;
            if (String.IsNullOrEmpty(strID))
                return false;

            // Use IdnMapping class to convert Unicode domain names. 
            try
            {
                strID = Regex.Replace(strID, @"(@)(.+)$", IDvalidator.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (m_bIsInvalid)
                return false;

            try
            {
                return Regex.IsMatch(strID,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private static string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                m_bIsInvalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}
