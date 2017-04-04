using EvilCorp.SlackStorage.LoggingService.DomainTypes;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace EvilCorp.SlackStorage.LoggingService.DataAccess
{
    public class LogRepository : ILogRepository
    {
        private readonly IMongoClient _client;
        
        public LogRepository(IMongoClient client)
        {
            _client = client;
        }

        public async Task Add(LogEntry log)
        {
            try
            {
                var db = _client.GetDatabase("LoggingService");
                var collection = db.GetCollection<LogEntry>("Logs");

                await collection.InsertOneAsync(log);
            }
            catch (Exception exception)
            {
                throw new InvalidProgramException("There was a problem inserting log entry.", exception);
            }
        }
    }
}
