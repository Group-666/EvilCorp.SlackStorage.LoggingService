using EvilCorp.SlackStorage.LoggingService.DomainTypes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;
using System;

namespace EvilCorp.SlackStorage.LoggingService.DataAccess
{
    public class LogEntryDto
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement]
        public string Component { get; set; }
        [BsonElement]
        public string Message { get; set; }
        [BsonElement]
        public DateTime Timestamp { get; set; }
        [BsonElement]
        public LogLevel Level { get; set; }

        public static LogEntry Parse(LogEntryDto log) =>
            new LogEntry(log.Component, log.Message, log.Timestamp, log.Level);

        public static LogEntryDto Parse(LogEntry log) =>
            new LogEntryDto
            {
                Component = log.Component,
                Message = log.Message,
                Timestamp = log.Timestamp,
                Level = log.Level
            };
    }
}
