using Microsoft.Practices.Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Login.Services.Client
{
    public abstract class RequestBase
    {
        protected ILoggerFacade m_Logger;

        public RequestBase(ILoggerFacade logger)
        {
            m_Logger = logger;
        }
    }
}
