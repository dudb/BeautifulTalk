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
    public class GetRoomInfoService : IGetRoomInfoService
    {
        public RoomEntity GetRoomInfo(IList<string> arMembers)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(10);
                    dynamic JParams = new JObject();
                    JParams.membersids = new JArray(arMembers).ToString();

                    var JsonParameters = JsonConvert.SerializeObject(JParams);
                    var postdataString = new StringContent(JsonParameters, new UTF8Encoding(), "application/json");

                    var responseMessage = httpClient.PostAsync(
                        string.Format("{0}{1}", BeautifulTalkProtocolSet.ServerURIwithPort, BeautifulTalkProtocolSet.GetRoomInfoURI),
                        postdataString).Result;

                    var JRoom =JsonConvert.DeserializeObject(responseMessage.Content.ReadAsStringAsync().Result);
                    return JsonConvert.DeserializeObject<RoomEntity>(JRoom.ToString());
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
