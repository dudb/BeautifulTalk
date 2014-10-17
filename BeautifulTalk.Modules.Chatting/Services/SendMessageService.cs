using BeautifulTalk.Modules.Chatting.Models;
using BeautifulTalkInfrastructure.AliveInformation;
using BeautifulTalkInfrastructure.DataModels;
using BeautifulTalkInfrastructure.Interfaces;
using BeautifulTalkInfrastructure.Logger;
using BeautifulTalkInfrastructure.ProtocolFormat;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BeautifulTalk.Modules.Chatting.Services
{
    public class SendMessageService : ISendMessageService
    {
        public SendMessageService() { }
        public SendingMsgResult SendMessage(SendingMsg sendingMsg)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(10);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthRepository.AccessInfo.TokenType, AuthRepository.AccessInfo.AccessToken);
                    //TransmitMsgRequest.Headers.Add(HttpRequestHeader.Authorization, string.Format("{0} {1}", accessInfo.Token_Type, accessInfo.Access_Token));
                    //string postData = String.Format(BeautifulTalkProtocolSet.PublishFormat, BeautifulTalkProtocolSet.OS, BeautifulTalkProtocolSet.ReleaseVersion,
                    //BeautifulTalkProtocolSet.SdkVersion, Environment.UserName, strToSIDs, strRoomSID, nContentType, strContent);

                    dynamic Parameters = new JObject();
                    Parameters.tosids = new JArray(sendingMsg.ToSIDs.ToArray());
                    Parameters.fromsid = sendingMsg.FromSID;
                    Parameters.roomsid = sendingMsg.RoomSID;
                    Parameters.contenttype = sendingMsg.ContentType;
                    Parameters.content = sendingMsg.Content;

                    var JsonParameters = JsonConvert.SerializeObject(Parameters);
                    var postdataString = new StringContent(JsonParameters, new UTF8Encoding(), "application/json");

                    var responseMessage = httpClient.PostAsync(
                        string.Format("{0}{1}", BeautifulTalkProtocolSet.ServerURIwithPort, BeautifulTalkProtocolSet.SendMsgURI),
                        postdataString).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var JResult = JsonConvert.DeserializeObject(responseMessage.Content.ReadAsStringAsync().Result);
                        return JsonConvert.DeserializeObject<SendingMsgResult>(JResult.ToString());
                    }
                }
            }
            catch (Exception unExpectedException)
            {
                GlobalLogger.Log(unExpectedException.Message);
            }

            return new SendingMsgResult();
        }
    }
}
