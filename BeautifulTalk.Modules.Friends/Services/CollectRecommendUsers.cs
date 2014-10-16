using BeautifulDB.Entities;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace BeautifulTalk.Modules.Friends.Services
{
    public class CollectRecommendUsers : ICollectRecommendUsers
    {
        public void Collect(int nUsersCount, string strMySID , RecommendFriendSummaryCollection recommendFriends)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(10);
                    dynamic parameter = new JObject();
                    parameter.count = 20;
                    parameter.fromsid = strMySID;
                    var JsonParameters = JsonConvert.SerializeObject(parameter);
                    var postdataString = new StringContent(JsonParameters, new UTF8Encoding(), "application/json");

                    var responseMessage = httpClient.PostAsync(
                        string.Format("{0}{1}", BeautifulTalkProtocolSet.ServerURIwithPort, BeautifulTalkProtocolSet.GetRandomUsersInfoURI),
                        postdataString).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var JRandomUsers = JsonConvert.DeserializeObject(responseMessage.Content.ReadAsStringAsync().Result);
                        var JArrayRandomUsers = JsonConvert.DeserializeObject<JArray>(JRandomUsers.ToString());

                        foreach (JObject Juser in JArrayRandomUsers)
                        {
                            var RecommendUser = JsonConvert.DeserializeObject<UserEntity>(Juser.ToString());

                            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new ThreadStart(() =>
                            {
                                recommendFriends.Add
                                    (new RecommendFriendSummary(RecommendUser.ThumbnailPath, RecommendUser.UserId,
                                        RecommendUser.Sid, RecommendUser.Comment, RecommendUser.NickName));
                            }));
                        }
                    }
                }
            }
            catch (Exception unExpectedException)
            {
                GlobalLogger.Log(unExpectedException.Message);
            }
        }
    }
}
