using BeautifulTalk.Modules.Login.Models;
using BeautifulTalkInfrastructure.ProtocolFormat;
using CommonUtility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BeautifulTalk.Modules.Login.Services.Client
{
    public class CollectInterestCategoriesService : ICollectInterestCategoriesService
    {
        public InterestCategoryCollection CollectCategories()
        {
            InterestCategoryCollection Categories = new InterestCategoryCollection();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(10);
                    var responseMessage = httpClient.GetAsync(
                        string.Format("{0}{1}",BeautifulTalkProtocolSet.ServerURIwithPort, BeautifulTalkProtocolSet.GetInterestsURI)).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        string strInterests = responseMessage.Content.ReadAsStringAsync().Result;
                        JValue JValueInterests = JsonConvert.DeserializeObject<JValue>(strInterests);
                        JArray JArrayInterests = JsonConvert.DeserializeObject<JArray>(JValueInterests.Value.ToString());

                        foreach (JObject JInterest in JArrayInterests)
                        {
                            var Interest = JsonConvert.DeserializeObject<InterestCategory>(JInterest.ToString());
                            Categories.Add(Interest);
                        }
                    }
                    else
                    {
                        throw new ArgumentNullException("Get Interests failed.");
                    }
                }
            }
            catch (FaultException serviceException)
            {
                throw serviceException;
            }
            catch (Exception unExpectedException)
            {
                throw unExpectedException;
            }

            return Categories;
        }
    }
}
