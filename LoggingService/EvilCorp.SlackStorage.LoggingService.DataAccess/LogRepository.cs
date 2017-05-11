using EvilCorp.SlackStorage.LoggingService.DomainTypes;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

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
                var collection = db.GetCollection<LogEntryDto>("Logs");

                await collection.InsertOneAsync(LogEntryDto.Parse(log));
            }
            catch (Exception exception)
            {
                throw new InvalidProgramException("There was a problem inserting log entry.", exception);
            }
        }

        public async Task<IEnumerable<LogEntry>> GetAll()
        {
            try
            {
                var db = _client.GetDatabase("LoggingService");
                var collection = db.GetCollection<LogEntryDto>("Logs");
                var documents = await collection.Find(Builders<LogEntryDto>.Filter.Empty).ToListAsync();

                return documents.Select(d => LogEntryDto.Parse(d));
            }
            catch (Exception exception)
            {
                throw new InvalidProgramException("There was a problem getting all log entries.", exception);
            }
        }
    }
}
