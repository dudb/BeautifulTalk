using BeautifulTalkInfrastructure.ProtocolFormat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalkInfrastructure.AliveInformation
{
    public class AccessInformation
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }

        public AccessInformation(string strAccessToken, string strTokenType)
        {
            this.AccessToken = strAccessToken;
            this.TokenType = strTokenType;
        }
    }
}
