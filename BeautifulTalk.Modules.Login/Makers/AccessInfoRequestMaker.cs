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
    public class AccessInfoRequestMaker : ICreatableRequest
    {
        public HttpWebRequest CreateRequest()
        {
            HttpWebRequest AcessInfoRequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}{1}", BeautifulTalkProtocolSet.ServerURIwithPort, BeautifulTalkProtocolSet.AccessTokenURI));
            AcessInfoRequest.ContentType = BeautifulTalkProtocolSet.ContentType;
            AcessInfoRequest.Method = HttpContentType.POST.ToString();
            AcessInfoRequest.Headers.Add(HttpRequestHeader.Authorization, BeautifulTalkProtocolSet.LoginAuthorizationValue);

            return AcessInfoRequest;
        }
    }
}
