using BeautifulTalkInfrastructure.Interfaces;
using BeautifulTalkInfrastructure.ProtocolFormat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Login.Makers
{
    public class MQKeyRequestMaker : ICreatableRequest
    {
        private CommonToken m_CommonToken;
        private string m_strAccessToken;
        public MQKeyRequestMaker(CommonToken commonToken, string strAccessToken)
        {
            m_CommonToken = commonToken;
            m_strAccessToken = strAccessToken;
        }

        public HttpWebRequest CreateRequest()
        {
            HttpWebRequest MQKeyRequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}{1}", BeautifulTalkProtocolSet.ServerURIwithPort, BeautifulTalkProtocolSet.GetMQInfoURI));
            MQKeyRequest.ContentType = BeautifulTalkProtocolSet.ContentType; ;
            MQKeyRequest.Method = HttpContentType.POST.ToString();
            MQKeyRequest.Headers.Add(HttpRequestHeader.Authorization, string.Format("{0} {1}", m_CommonToken, m_strAccessToken));

            return MQKeyRequest;
        }
    }
}
