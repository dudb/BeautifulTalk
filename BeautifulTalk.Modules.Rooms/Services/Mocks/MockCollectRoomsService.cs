using BeautifulDB.Entities;
using BeautifulTalk.Modules.Rooms.Models;
using Microsoft.Practices.Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BeautifulTalk.Modules.Rooms.Services.Mocks
{
    public class MockCollectRoomsService : ICollectRoomsService
    {
        private readonly ILoggerFacade m_Logger;
        public MockCollectRoomsService(ILoggerFacade logger)
        {
            if (null == logger) throw new ArgumentNullException("logger");

            m_Logger = logger;
        }
        public void CollectRooms(RoomCollection rooms, string strMySid)
        {
            m_Logger.Log("Mocking CollectRooms Raised", Category.Info, Priority.None);
            /*
            rooms.Add(new RoomEntity
                ("0", new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/1.png", UriKind.Relative))
                , "Paul", 0,2, "Bye! See you!", DateTime.Now.ToShortDateString()));
            rooms.Add(new RoomEntity
                ("1", new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/2.png", UriKind.Relative)),
                "Jacob, Angel ,Lucas,Grace  ", 5, 0, "Really?", DateTime.Now.ToShortDateString()));
            rooms.Add(new RoomEntity
                ("2", new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/3.png", UriKind.Relative)),
                "Michael , Landon ,Isaac ", 4, 38, "When is your birthday?", DateTime.Now.ToShortDateString()));
            rooms.Add(new RoomEntity
                ("3", new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/4.png", UriKind.Relative)),
                "Alexander , Evan ,Isaiah,Taylor,Hannah  ", 6, 0, "Oh, i see...", DateTime.Now.ToShortDateString()));
            rooms.Add(new RoomEntity
                ( "4", new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/5.png", UriKind.Relative)),
                "William , Mason ,Emma ", 4, 17, "Anyway, i will get there", DateTime.Now.ToShortDateString()));
            rooms.Add(new RoomEntity
                ( "5", new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/6.png", UriKind.Relative)),
                "Joshua , Gavin ,Olivia ", 4, 0, "Shut up!!!", DateTime.Now.ToShortDateString()));
            rooms.Add(new RoomEntity
                ("6", new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/7.png", UriKind.Relative)),
                "Daniel , Nicholas ,Sophia ", 4, 0, "Can i see?", DateTime.Now.ToShortDateString()));
            rooms.Add(new RoomEntity
                ( "7", new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/8.png", UriKind.Relative)),
                "Jayden , Caleb ,Ava ", 4, 0, "i wanaa go home....ohoh", DateTime.Now.ToShortDateString()));
            rooms.Add(new RoomEntity
                ( "8", new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/9.png", UriKind.Relative)),
                "Noah , Jonathan ,Emily ", 4, 0, "tell me how am i supposed live without you..?", DateTime.Now.ToShortDateString()));
            rooms.Add(new RoomEntity
                ( "9", new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/10.png", UriKind.Relative)),
                "Anthony , Dylan ,Madison ", 4, 0, "Bye! bye...bye...", DateTime.Now.ToShortDateString()));
            rooms.Add(new RoomEntity
                ( "10", new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/11.png", UriKind.Relative)),
                "Christopher , Tyler ,Abigail ", 4, 0, "i'm Crying now...", DateTime.Now.ToShortDateString()));
            rooms.Add(new RoomEntity
                ( "11", new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/12.png", UriKind.Relative)),
                "Aiden , Samuel ,Chloe ", 4, 0, "Did you see that?", DateTime.Now.ToShortDateString()));
            rooms.Add(new RoomEntity
                ( "12", new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/13.png", UriKind.Relative)),
                "Matthew , John ,Mia ", 4, 0, "Maybe. On september?", DateTime.Now.ToShortDateString()));
            rooms.Add(new RoomEntity
                ("13", new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/14.png", UriKind.Relative)),
                "David , Jackson ,Elizabeth ", 4, 0, "How are you David?", DateTime.Now.ToShortDateString()));
            rooms.Add(new RoomEntity
                ("14", new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/15.png", UriKind.Relative)),
                "Andrew , Nathan ,Alexis ", 4, 0, "Just 3 days remain..", DateTime.Now.ToShortDateString()));
            rooms.Add(new RoomEntity
                ( "15", new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/16.png", UriKind.Relative)),
                "Joseph , Gabriel ,Addison ", 4, 0, "Remember!", DateTime.Now.ToShortDateString()));
            rooms.Add(new RoomEntity
                ( "16", new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/17.png", UriKind.Relative)),
                "Logan , Elijah ,Ella ", 4, 0, "Perfect! wonderful!!", DateTime.Now.ToShortDateString()));
            rooms.Add(new RoomEntity
                ("17", new BitmapImage(new Uri(@"/BeautifulTalk.Modules.Rooms;component/Resources/Images/Mocks/18.png", UriKind.Relative)),
                "Ryan , Benjamin ,Samantha ", 4, 0, "let me see your info..", DateTime.Now.ToShortDateString()));*/
        }
    }
}
