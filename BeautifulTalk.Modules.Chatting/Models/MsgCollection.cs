using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Chatting.Models
{
    public class MsgCollection : ObservableCollection<Msg>
    {
        public MsgCollection(IEnumerable<Msg> msgs) : base(msgs)
        { 
        
        }
    }
}
