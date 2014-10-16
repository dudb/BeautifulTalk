using BeautifulTalk.Modules.Chatting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BeautifulTalk.Modules.Chatting.Selectors
{
    public class MsgDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DateMsgDataTemplate { get; set; }
        public DataTemplate ChatMsgDataTemplate { get; set; }
        public DataTemplate OpponentMsgDataTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is OpponentMsg)
            {
                return OpponentMsgDataTemplate;
            }
            else if (item is ChatMsg)
            {
                return ChatMsgDataTemplate;
            }
            else if (item is DateMsg)
            {
                return DateMsgDataTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
