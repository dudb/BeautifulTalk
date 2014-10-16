using Microsoft.Practices.Prism.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalkInfrastructure.Logger
{
    public class TimeBasedLoggerAdapter : ILoggerFacade
    {
        private static TimeBasedLoggerAdapter m_TimeBasedLogger = new TimeBasedLoggerAdapter();
        public void Log(string message, Category category, Priority priority)
        {
            string messageToLog = String.Format(CultureInfo.InvariantCulture, "[{0}] ({1:u}) ({2}) : {3}.",
                category.ToString().ToUpper(CultureInfo.InvariantCulture), DateTime.Now, priority.ToString(), message);

            Console.WriteLine(messageToLog);
        }

        public static ILoggerFacade CreateLogger()
        {
            if (null == m_TimeBasedLogger) { m_TimeBasedLogger = new TimeBasedLoggerAdapter(); }

            return m_TimeBasedLogger;
        }
    }
}
