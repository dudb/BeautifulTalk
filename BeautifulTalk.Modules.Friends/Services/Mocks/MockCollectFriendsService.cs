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
                friends.Add(new Friend
                (ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/1.png"),
                "", "Paul", "Bye! See you!", TDD));
                friends.Add(new Friend
                    (ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/2.png"),
                    "", "Jacob", "Really?", CI));
                friends.Add(new Friend
                    (ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/3.png"),
                    "", "Michael", "When is your birthday?", CI));
                friends.Add(new Friend
                    (ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/4.png"),
                    "", "Alexander", "Oh, i see...", SA));
                friends.Add(new Friend
                    (ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/5.png"),
                    "", "William", "Anyway, i will get there", SE));
                friends.Add(new Friend
                    (ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/6.png"),
                    "", "Joshua", "Shut up!!!", DP));
                friends.Add(new Friend
                    (ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/7.png"),
                    "", "Daniel", "Can i see?", TDD));
                friends.Add(new Friend
                    (ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/8.png"),
                    "", "Jayden ", "i wanaa go home....ohoh", DP));
                friends.Add(new Friend
                    (ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/9.png"),
                    "", "Noah ", "tell me how am i supposed live without you..?", SA));
                friends.Add(new Friend
                    (ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/10.png"),
                    "", "Anthony", "Bye! bye...bye...", CI));
                friends.Add(new Friend
                    (ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/11.png"),
                    "", "Christopher", "i'm Crying now...", SE));
                friends.Add(new Friend
                    (ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/12.png"),
                    "", "Aiden", "Did you see that?", SA));
                friends.Add(new Friend
                    (ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/13.png"),
                    "", "Matthew", "Maybe. On september?", SE));
                friends.Add(new Friend
                    (ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/14.png"),
                    "", "David", "How are you David?", SA));
                friends.Add(new Friend
                    (ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/15.png"),
                    "", "Andrew", "Just 3 days remain..", DP));
                friends.Add(new Friend
                    (ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/16.png"),
                    "", "Joseph ", "Remember!", TDD));
                friends.Add(new Friend
                    (ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/17.png"),
                    "", "Logan", "Perfect! wonderful!!", CI));
                friends.Add(new Friend
                    (ToBitmapImgConverter.LoadImage(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/18.png"),
                    "", "Ryan", "let me see your info..", SA));
            }));
        }
    }
}
