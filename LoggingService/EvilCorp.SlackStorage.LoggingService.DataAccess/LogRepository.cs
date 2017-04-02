using EvilCorp.SlackStorage.LoggingService.DomainTypes;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace EvilCorp.SlackStorage.LoggingService.DataAccess
{
    public class LogRepository : ILogRepository
    {
        private readonly string _connectionString;
        
        public LogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Add(LogEntry log)
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase("LoggingService");
            var collection = db.GetCollection<LogEntry>("Logs");

            await collection.InsertOneAsync(log);
        }
    }
}
