using BeautifulTalk.Modules.Login.Models;
using Microsoft.Practices.Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Login.Services.Client.Mocks
{
    public class MockTrackConnectionService : ITrackSuccessConnectionService
    {
        private readonly ILoggerFacade m_Logger;
        public MockTrackConnectionService(ILoggerFacade logger)
        {
            if (null == logger) throw new ArgumentNullException("logger");

            m_Logger = logger;
        }
        public Identifications GetListOfSuccessConnections()
        {
            m_Logger.Log("Mocking GetListOfSuccessConnections Raised", Category.Info, Priority.None);

            return new Identifications
                {
                    "service@shop.com",
                    "obk6438@naver.com",
                    "lee1829@mapps.com",
                    "kim4332@gmail.com",
                    "hominst@yahoo.com",
                    "jung282@lycos.com",
                    "RayerGood@hanmail.com"

                };
        }
    }
}
