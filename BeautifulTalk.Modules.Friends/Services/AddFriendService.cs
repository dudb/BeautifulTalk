using BeautifulTalk.Modules.Friends.Models;
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
    public class AddFriendService : IAddFriendService
    {
        public bool AddFriend(string strMySID, string strFriendSID)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(10);
                    dynamic parameter = new JObject();
                    parameter.usersid = strMySID;
                    parameter.friendsid = strFriendSID;
                    var JsonParameters = JsonConvert.SerializeObject(parameter);
                    var postdataString = new StringContent(JsonParameters, new UTF8Encoding(), "application/json");

                    var responseMessage = httpClient.PostAsync(
                        string.Format("{0}{1}", BeautifulTalkProtocolSet.ServerURIwithPort, BeautifulTalkProtocolSet.AddFriendURI),
                        postdataString).Result;

                    return responseMessage.IsSuccessStatusCode;
                }
            }
            catch (Exception unExpectedException)
            {
                GlobalLogger.Log(unExpectedException.Message);
                return false;
            }
        }
    }
}
