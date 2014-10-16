using BeautifulTalk.Modules.Friends.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace BeautifulTalk.Modules.Friends.Services.Mocks
{
    public class MockCollectRecommendUsers : ICollectRecommendUsers
    {
        public void Collect(int nUsersCount, string strMySID, RecommendFriendSummaryCollection recommendFriends)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new ThreadStart(() =>
            {
                recommendFriends.Add(new RecommendFriendSummary(null, "jjin@gmail.com", "", "i am sad...", "Y2k"));
                recommendFriends.Add(new RecommendFriendSummary(null, "fight@gmail.com", "", "studying..", "HKO"));
                recommendFriends.Add(new RecommendFriendSummary(null, "canyou@gmail.com", "", "d-5", "Beyonce"));
                recommendFriends.Add(new RecommendFriendSummary(null, "jskk22@gmail.com", "", "prepare soccer", "sigh"));
                recommendFriends.Add(new RecommendFriendSummary(null, "solid44@gmail.com", "", "sleeping~", "Catch"));
                recommendFriends.Add(new RecommendFriendSummary(null, "yes24@gmail.com", "", "gaming", "fake"));
                recommendFriends.Add(new RecommendFriendSummary(null, "yahomy@gmail.com", "", "Running", "sissors"));
                recommendFriends.Add(new RecommendFriendSummary(null, "james@gmail.com", "", "reading", "nicole"));
            }));
        }
    }
}
