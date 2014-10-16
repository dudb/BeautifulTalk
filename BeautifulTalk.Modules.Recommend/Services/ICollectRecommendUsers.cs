using BeautifulTalk.Modules.Recommend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Recommend.Services
{
    public interface ICollectRecommendUsers
    {
        FriendSummaryCollection Collect(int nUsersCount, string strMyID);
    }
}
