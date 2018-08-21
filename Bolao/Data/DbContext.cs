using System.Configuration;
using MongoDB.Driver;


namespace Bolao.Data
{
    public class MongoDataContext
    {
        public MongoDataContext()
            : this("MongoDb")
        {
        }

        public MongoDataContext(string connectionName)
        {
            var url = 
                ConfigurationManager.ConnectionStrings["ConnectionStrings"].ConnectionString;

            var mongoUrl = new MongoUrl(url);
            IMongoClient client = new MongoClient(mongoUrl);
            MongoDatabase = client.GetDatabase(mongoUrl.DatabaseName);    
        }

        public IMongoDatabase MongoDatabase { get; }
    }
}