using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalkInfrastructure.Logger
{
    public class GlobalLogger
    {
        private static string LogFilePath;
        private static readonly object locker = new object();
        static GlobalLogger()
        {
            string strToday = DateTime.Today.ToString("yyyyMMdd");
            LogFilePath = string.Format("{0}{1}{2}.txt", AppDomain.CurrentDomain.BaseDirectory, "log", strToday);
        }

        public static void Log(string strLogMessage)
        {
            lock (locker)
            {
                using (StreamWriter sw = File.AppendText(LogFilePath))
                {
                    sw.WriteLine("{0}\n", strLogMessage);
                }
            }
        }
    }
}
