using BeautifulTalk.Modules.Friends.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Friends.Services
{
    public interface ICollectRecommendUsers
    {
        void Collect(int nUsersCount, string strMySID, RecommendFriendSummaryCollection recommendFriends);
    }
}
