using BeautifulTalkInfrastructure.AliveInformation;
using BeautifulTalkInfrastructure.ProtocolFormat;
using Microsoft.Practices.Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Login.Services.Client.Mocks
{
    public class MockRequestAccessInfoService : RequestBase, IRequestAccessInfoService
    {
        public MockRequestAccessInfoService(ILoggerFacade logger) : base(logger) { }
        public AccessInformation RequestAccessInfo(string strId, string strPassword)
        {
            this.m_Logger.Log("Mocking RequestAccessInfo Raised", Category.Info, Priority.None);
            
            return new AccessInformation(Guid.NewGuid().ToString(), CommonToken.bearer.ToString());
        }
    }
}
