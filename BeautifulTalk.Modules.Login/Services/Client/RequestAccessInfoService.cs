using BeautifulTalkInfrastructure.AliveInformation;
using BeautifulTalkInfrastructure.Logger;
using BeautifulTalkInfrastructure.ProtocolFormat;
using Microsoft.Practices.Prism.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Login.Services.Client
{
    public class RequestAccessInfoService : RequestBase, IRequestAccessInfoService
    {
        public RequestAccessInfoService(ILoggerFacade logger) : base(logger) { }
        public AccessInformation RequestAccessInfo(string strId, string strPassword)
        {
            this.m_Logger.Log("RequestAccessInfo Raised", Category.Info, Priority.None);

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(10);
                    dynamic Parameters = new JObject();
                    Parameters.granttype = "password";
                    Parameters.applicationid = BeautifulTalkProtocolSet.ApplicationId;
                    Parameters.id = strId;
                    Parameters.password = strPassword;
                    var JsonParameters = JsonConvert.SerializeObject(Parameters);
                    var postdataString = new StringContent(JsonParameters, new UTF8Encoding(), "application/json");
                    var responseMessage = httpClient.PostAsync(
                        string.Format("{0}{1}", BeautifulTalkProtocolSet.ServerURIwithPort, BeautifulTalkProtocolSet.AccessTokenURI), postdataString).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var JAccessInformation = JsonConvert.DeserializeObject(responseMessage.Content.ReadAsStringAsync().Result);
                        return JsonConvert.DeserializeObject<AccessInformation>(JAccessInformation.ToString());
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
