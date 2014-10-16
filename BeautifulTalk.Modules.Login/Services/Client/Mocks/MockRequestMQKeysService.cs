using BeautifulTalkInfrastructure.AliveInformation;
using BeautifulTalkInfrastructure.Generators;
using BeautifulTalkInfrastructure.ProtocolFormat;
using Microsoft.Practices.Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Login.Services.Client.Mocks
{
    public class MockRequestMQKeysService : RequestBase, IRequestMQKeysService
    {
        public MockRequestMQKeysService(ILoggerFacade logger) : base(logger) { }
        public MQKeySet RequestMQKeys(string strTokenType, string strAccessToken)
        {
            this.m_Logger.Log("Mocking RequestMQKeys Raised", Category.Info, Priority.None);

            MQKeySet MockMQKeySet = new MQKeySet();
            MockMQKeySet.Account = "service@shop.com";
            MockMQKeySet.Message = "successed sign in";
            MockMQKeySet.MqKey = "service@shop.com";
            MockMQKeySet.MqSid = "service@shop.com";
            MockMQKeySet.NickName = "Anonymous User";
            MockMQKeySet.Status = 200;
            MockMQKeySet.Type = 1;
            MockMQKeySet.UserSid = Guid.NewGuid().ToString();

            return MockMQKeySet;
        }
    }
}
