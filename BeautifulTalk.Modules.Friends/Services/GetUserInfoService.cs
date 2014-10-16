using BeautifulDB.Entities;
using BeautifulTalkInfrastructure.Logger;
using BeautifulTalkInfrastructure.ProtocolFormat;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Friends.Services
{
    public class GetUserInfoService : IGetUserInfoService
    {
        public UserEntity GetUserInfo(string strUserSID)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(10);
                    dynamic parameter = new JObject();
                    parameter.usersid = strUserSID;
                    var JsonParameters = JsonConvert.SerializeObject(parameter);
                    var postdataString = new StringContent(JsonParameters, new UTF8Encoding(), "application/json");

                    var responseMessage = httpClient.PostAsync(
                        string.Format("{0}{1}", BeautifulTalkProtocolSet.ServerURIwithPort, BeautifulTalkProtocolSet.GetUserInfoURI),
                        postdataString).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var JUserInfo = JsonConvert.DeserializeObject(responseMessage.Content.ReadAsStringAsync().Result);
                        return JsonConvert.DeserializeObject<UserEntity>(JUserInfo.ToString());
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
