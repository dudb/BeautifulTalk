using BeautifulTalk.Modules.Login.Makers;
using BeautifulTalkInfrastructure.AliveInformation;
using BeautifulTalkInfrastructure.Interfaces;
using BeautifulTalkInfrastructure.Logger;
using BeautifulTalkInfrastructure.ProtocolFormat;
using Microsoft.Practices.Prism.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Login.Services.Client
{
    public class RequestMQKeysService : RequestBase, IRequestMQKeysService
    {
        public RequestMQKeysService(ILoggerFacade logger) : base(logger) { }
        public MQKeySet RequestMQKeys(string strTokenType, string strAccessToken)
        {
            this.m_Logger.Log("RequestMQKeys Raised", Category.Info, Priority.None);

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(10);
                    dynamic Parameters = new JObject();
                    Parameters.tokentype = strTokenType;
                    Parameters.accesstoken = strAccessToken;
                    var JsonParameters = JsonConvert.SerializeObject(Parameters);
                    var postdataString = new StringContent(JsonParameters, new UTF8Encoding(), "application/json");
                    var responseMessage = httpClient.PostAsync(
                        string.Format("{0}{1}", BeautifulTalkProtocolSet.ServerURIwithPort, BeautifulTalkProtocolSet.GetMQInfoURI), postdataString).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var JMQInfo = JsonConvert.DeserializeObject(responseMessage.Content.ReadAsStringAsync().Result);
                        return JsonConvert.DeserializeObject<MQKeySet>(JMQInfo.ToString());
                    }
                }
            }
            catch (Exception unExpectedException)
            {
                GlobalLogger.Log(unExpectedException.Message);
            }

            return null;
        }
    }
}
