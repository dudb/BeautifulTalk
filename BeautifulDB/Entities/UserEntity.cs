using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulDB.Entities
{
    public class UserEntity
    {
        public ObjectId Id { get; private set; }
        public IList<string> InterestIds { get; set; }
        public byte[] Thumbnail { get; set; }
        public string UserId { get; set; }
        public string NickName { get; set; }
        public string Comment { get; set; }
        public string Sid { get; set; }

        public UserEntity(string strUserId, string strSid, string strNickName, IList<string> InterestIds)
        {
            this.UserId = strUserId;
            this.Sid = strSid;
            this.NickName = strNickName;
            this.InterestIds = InterestIds;
        }
    }
}
