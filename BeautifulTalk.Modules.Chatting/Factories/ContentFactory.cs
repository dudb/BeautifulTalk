using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Chatting.Factories
{
    public abstract class ContentFactory
    {
        protected string m_strContent;

        public ContentFactory(string strContent)
        {
            this.m_strContent = strContent;
        }
    }
}
