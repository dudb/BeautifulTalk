using BeautifulTalk.Modules.Recommend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Recommend.Services.Mocks
{
    public class MockCollectRecommendUsers : ICollectRecommendUsers
    {
        public FriendSummaryCollection Collect(int nUsersCount, string strMyID)
        {
            FriendSummaryCollection RecommendUsers = new FriendSummaryCollection();

            RecommendUsers.Add(new FriendSummary(null, "jjin@gmail.com", "i am sad...", "Y2k"));
            RecommendUsers.Add(new FriendSummary(null, "fight@gmail.com", "studying..", "HKO"));
            RecommendUsers.Add(new FriendSummary(null, "canyou@gmail.com", "d-5", "Beyonce"));
            RecommendUsers.Add(new FriendSummary(null, "jskk22@gmail.com", "prepare soccer", "sigh"));
            RecommendUsers.Add(new FriendSummary(null, "solid44@gmail.com", "sleeping~", "Catch"));
            RecommendUsers.Add(new FriendSummary(null, "yes24@gmail.com", "gaming", "fake"));
            RecommendUsers.Add(new FriendSummary(null, "yahomy@gmail.com", "Running", "sissors"));
            RecommendUsers.Add(new FriendSummary(null, "james@gmail.com", "reading", "nicole"));
            return RecommendUsers;
        }
    }
}
