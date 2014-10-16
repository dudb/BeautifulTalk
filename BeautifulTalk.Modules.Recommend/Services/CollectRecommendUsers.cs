using BeautifulTalk.Modules.Recommend.Models;
using BeautifulTalkInfrastructure.ProtocolFormat;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Recommend.Services
{
    public class CollectRecommendUsers : ICollectRecommendUsers
    {
        public FriendSummaryCollection Collect(int nUsersCount, string strMyID)
        {
            FriendSummaryCollection RecommendUsers = new FriendSummaryCollection();

            using (var httpClientt = new HttpClient())
            {
                dynamic parameter = new JObject();
                parameter.count = 10;
                parameter.fromID = strMyID;
                var JsonParameters = JsonConvert.SerializeObject(parameter);
                var postdataString = new StringContent(JsonParameters, new UTF8Encoding(), "application/json");

                var responseMessage = httpClientt.PostAsync(
                    string.Format("{0}{1}", BeautifulTalkProtocolSet.ServerURIwithPort, BeautifulTalkProtocolSet.GetRandomUsersInfoURI),
                    postdataString).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var JRandomUsers = JsonConvert.DeserializeObject(responseMessage.Content.ReadAsStringAsync().Result);
                    var JArrayRandomUsers = JsonConvert.DeserializeObject<JArray>(JRandomUsers.ToString());

                    foreach (JObject Juser in JArrayRandomUsers)
                    {
                        byte[] arThumbnail = null;
                        if (false == string.IsNullOrEmpty(Juser[UserParameters.Thumbnail].ToString())) { arThumbnail = (byte[])Juser[UserParameters.Thumbnail]; }
                        string strUserID = Juser[UserParameters.UserID].ToString();
                        string strNickName = Juser[UserParameters.NickName].ToString();
                        string strComment = Juser[UserParameters.Comment].ToString();

                        RecommendUsers.Add(new FriendSummary(arThumbnail, strUserID, strComment, strNickName));
                    }
                }
            }

            return RecommendUsers;
        }
    }
}
