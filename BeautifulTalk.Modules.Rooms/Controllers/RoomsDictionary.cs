using BeautifulTalk.Modules.Chatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Rooms.Controllers
{
    public class RoomsDictionary : IRoomsDictionaryBehavior<string, ChattingShellView>
    {
        private Dictionary<string, ChattingShellView> m_RoomsDictionary;

        public RoomsDictionary()
        {
            this.m_RoomsDictionary = new Dictionary<string, ChattingShellView>();
        }

        public bool Register(string key, ChattingShellView target)
        {
            if (null == key) throw new ArgumentNullException("key");
            if (null == target) throw new ArgumentNullException("target");

            if (false == this.m_RoomsDictionary.ContainsKey(key))
            {
                this.m_RoomsDictionary.Add(key, target);
                return true;
            }

            return false;
        }

        public bool Remove(string key)
        {
            if (null == key) throw new ArgumentNullException("key");

            this.m_RoomsDictionary.Remove(key);
            return true;
        }

        public bool DoesExist(string key)
        {
            if (null == key) throw new ArgumentNullException("key");

            return this.m_RoomsDictionary.ContainsKey(key);
        }

        public ChattingShellView GetValue(string key)
        {
            try
            {
                return this.m_RoomsDictionary[key];
            }
            catch(ArgumentNullException argsNullException)
            {
                throw argsNullException;  
            }
            catch(KeyNotFoundException keyNotFoundException)
            {
                throw keyNotFoundException;
            }
        }
    }
}
