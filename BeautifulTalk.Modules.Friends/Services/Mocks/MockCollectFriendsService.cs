using BeautifulTalk.Modules.Friends.Models;
using CommonUtility;
using Microsoft.Practices.Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace BeautifulTalk.Modules.Friends.Services.Mocks
{
    public class MockCollectFriendsService : ICollectFriendsService
    {
        internal const string SE = "Software Engineering";
        internal const string SA = "Service Architecture";
        internal const string CI = "Continuous Intergration";
        internal const string DP = "Design Patterns";
        internal const string TDD = "Test-Driven Development";

        private readonly ILoggerFacade m_Logger;
        public MockCollectFriendsService(ILoggerFacade logger)
        {
            this.m_Logger = logger;
        }
        public void CollectFriends(string strUserSID, FriendCollection friends)
        {
            m_Logger.Log("Mocking CollectFriends Raised", Category.Info, Priority.None);

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new ThreadStart(() =>
            {
                friends.Add(new Friend(null,"", "Paul", "Bye! See you!", TDD));
                friends.Add(new Friend(null,"", "Jacob", "Really?", CI));
                friends.Add(new Friend(null,"", "Michael", "When is your birthday?", CI));
                friends.Add(new Friend(null,"", "Alexander", "Oh, i see...", SA));
                friends.Add(new Friend(null,"", "William", "Anyway, i will get there", SE));
                friends.Add(new Friend(null,"", "Joshua", "Shut up!!!", DP));
                friends.Add(new Friend(null,"", "Daniel", "Can i see?", TDD));
                friends.Add(new Friend(null,"", "Jayden ", "i wanaa go home....ohoh", DP));
                friends.Add(new Friend(null,"", "Noah ", "tell me how am i supposed live without you..?", SA));
                friends.Add(new Friend(null,"", "Anthony", "Bye! bye...bye...", CI));
                friends.Add(new Friend(null,"", "Christopher", "i'm Crying now...", SE));
                friends.Add(new Friend(null,"", "Aiden", "Did you see that?", SA));
                friends.Add(new Friend(null,"", "Matthew", "Maybe. On september?", SE));
                friends.Add(new Friend(null,"", "David", "How are you David?", SA));
                friends.Add(new Friend(null,"", "Andrew", "Just 3 days remain..", DP));
                friends.Add(new Friend(null,"", "Joseph ", "Remember!", TDD));
                friends.Add(new Friend(null,"", "Logan", "Perfect! wonderful!!", CI));
                friends.Add(new Friend(null,"", "Ryan", "let me see your info..", SA));
            }));
        }
    }
}
