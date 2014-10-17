using BeautifulTalk.Modules.Chatting.Models;
using BeautifulTalkInfrastructure.DataModels;
using CommonUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Chatting.Services.Mocks
{
    public class MockLoadMessagesService : ILoadMessagesService
    {
        public IEnumerable<Msg> LoadMessages(string strRoomSID, long lCriterion)
        {
            /*
            MockMessages.Add(new DateMsg(0, null, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToFileTime()));

            MockMessages.Add(new ChatMsg(0, null, Encoding.UTF8.GetBytes("Hello! Evenryone. Today, I will introduce to our plan in the future."), 0,
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToFileTime(), "Dana,John,Paul", "Bill Gates",
                ToBitmapImgConverter.LoadImage(@"pack://application:,,,/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/2.png")));

            MockMessages.Add(new ChatMsg(0, null, Encoding.UTF8.GetBytes("Before to talk each other, May i drink some water?"), 0,
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToFileTime(), "Dana,John,Paul", "Bill Gates",
                ToBitmapImgConverter.LoadImage(@"pack://application:,,,/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/2.png")));

            MockMessages.Add(new OpponentMsg(0, null, Encoding.UTF8.GetBytes("Of course, sir. Let's drink some water!"), 0,
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToFileTime(), "Bill Gates,John,Paul", "Dana",
                ToBitmapImgConverter.LoadImage(@"pack://application:,,,/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/6.png")));

            MockMessages.Add(new OpponentMsg(0, null, Encoding.UTF8.GetBytes("I am glad to talk with you inside soft atmosphere^^."), 0,
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToFileTime(), "Bill Gates,Paul", "John",
                ToBitmapImgConverter.LoadImage(@"pack://application:,,,/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/5.png")));

            MockMessages.Add(new OpponentMsg(0, null, Encoding.UTF8.GetBytes("I'm curious about topic that will lead our company."), 0,
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToFileTime(), "Bill Gates,Paul", "Paul",
                ToBitmapImgConverter.LoadImage(@"pack://application:,,,/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/4.png")));

            MockMessages.Add(new ChatMsg(0, null, Encoding.UTF8.GetBytes("Calm down evenryone. Take it easy. Above all, we have plenty of information to be responsible for coming environment."), 0,
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToFileTime(), "Bill Gates", "Bill Gates",
                ToBitmapImgConverter.LoadImage(@"pack://application:,,,/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/2.png")));

            MockMessages.Add(new ChatMsg(0, null, Encoding.UTF8.GetBytes("Have you ever heard the word 'BDD'?"), 0,
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToFileTime(), "Bill Gates", "Bill Gates",
                ToBitmapImgConverter.LoadImage(@"pack://application:,,,/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/2.png")));
            */
            return null;
        }

        public IList<UnReadMsg> LoadUnReadMessages(string strRoomSID)
        {
            return new List<UnReadMsg>();
        }
    }
}
