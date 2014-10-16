using BeautifulTalkInfrastructure.ProtocolFormat;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Chatting.Models
{
    public abstract class Msg : BindableBase
    {
        public ContentType ContentType { get; private set; }
        public string Content { get; private set; }
        public Msg(string strContent, ContentType contentType)
        {
            this.Content = strContent;
            this.ContentType = contentType;
        }
    }
}
