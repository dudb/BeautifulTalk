using BeautifulTalkInfrastructure.AliveInformation;
using BeautifulTalkInfrastructure.DataModels;
using BeautifulTalkInfrastructure.Logger;
using BeautifulTalkInfrastructure.ProtocolFormat;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Chatting.Services
{
    public class ReadMessageService : IReadMessageService
    {
        public bool ReadMessages(IList<UnReadMsg> unReadMsgs)
        {
            try
            {
                if (0 == unReadMsgs.Count) return false;
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(10);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthRepository.AccessInfo.TokenType, AuthRepository.AccessInfo.AccessToken);

                    dynamic Parameters = new JObject();
                    Parameters.readmsgsids = string.Join(",", unReadMsgs.Select(m => m.Sid));
                    Parameters.roomsid = unReadMsgs[0].RoomSid;
                    Parameters.fromsid = unReadMsgs[0].FromSid;

                    var JsonParameters = JsonConvert.SerializeObject(Parameters);
                    var postdataString = new StringContent(JsonParameters, new UTF8Encoding(), "application/json");

                    var responseMessage = httpClient.PostAsync(
                        string.Format("{0}{1}", BeautifulTalkProtocolSet.ServerURIwithPort, BeautifulTalkProtocolSet.ReadMsgURI),
                        postdataString).Result;

                    return responseMessage.IsSuccessStatusCode;
                }
            }
            catch (Exception unExpectedException)
            {
                GlobalLogger.Log(unExpectedException.Message);
            }

            return false;
        }
    }
}
