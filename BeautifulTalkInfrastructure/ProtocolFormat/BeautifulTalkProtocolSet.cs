using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalkInfrastructure.ProtocolFormat
{
    public class MQInfoParameters
    {
        public string tokentype { get; set; }
        public string accesstoken { get; set; }
    }
    public class BeautifulTalkProtocolSet
    {
        public static int OS = 3;
        public static int Version = 7;
        public static int AppVersion = 1;

        public static string PublishURI = "/api/talk/publish";
        public static string AccessTokenURI = "/api/auth/token";
        public static string GetInterestsURI = "/api/repository/interests";
        public static string GetRandomUsersInfoURI = "/api/repository/randuserinfo";
        public static string GetRoomInfoURI = "/api/repository/roominfo";
        public static string SendMsgURI = "/api/mq/send";
        public static string GetFriendsInfoURI = "/api/repository/friendsinfo";
        public static string GetUserInfoURI = "/api/repository/userinfo";
        public static string GetMQInfoURI = "/api/auth/mq";
        public static string SignUpURI = "/api/reg/signup";
        public static string AddFriendURI = "/api/update/addfriend";
        public static string ReadURI = "/api/talk/read";
        public static string ContentType = "application/x-www-form-urlencoded";
        public static string ServerURIwithPort = "http://54.64.156.101:5001";
        public static string ServerURI = "http://54.64.156.101";
        public static string MQServerIP = "54.64.156.101";
        public static string ServerPort = "5001";

        public static string ApplicationId = "clfkml2k4f04gj90erj034j";
        public static string LoginAuthorizationValue = "Basic czZCaGRSa3F0MzpnWDFmQmF0M2JW";
    }

    public enum HttpContentType
    {
        GET,
        POST,
        PUT
    }

    public enum GrantToken
    {
        password,
        refresh_token
    }

    public enum CommonToken
    {
        bearer
    }

    public enum ContentType
    { 
        Text = 0,
        Image = 1,
        Media = 2
    }
    public enum MsgType
    {
        ChatData = 0,
        Read = 1,
        Leave = 2,
        Notice = 3
    }

    public enum MsgStatus
    {
        Failed = 0,
        Sending = 1,
        Sent = 2,
        Read = 3,
        Received = 4
    }
}
