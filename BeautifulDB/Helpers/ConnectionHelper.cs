using BeautifulDB.Configurations;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulDB.Helpers
{
    public class ConnectionHelper
    {
        private static MongoDatabase m_DB = null;
        private static object locker = new object();

        public static MongoDatabase DB
        { 
            get
            {
                lock (locker)
                {
                    if (null == m_DB)
                    {
                        var client = new MongoClient(MongoConfiguration.ConnectionString);
                        var server = client.GetServer();
                        m_DB = server.GetDatabase(DatabaseNames.BeautifulLocalDB);
                    }
                    return m_DB;
                }
            }
        }
    }
}
