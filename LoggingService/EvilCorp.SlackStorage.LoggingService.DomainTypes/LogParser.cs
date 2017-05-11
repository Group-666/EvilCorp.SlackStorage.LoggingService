using Newtonsoft.Json.Linq;
using System;

namespace EvilCorp.SlackStorage.LoggingService.DomainTypes
{
    public class LogParser
    {
        public static LogEntry Parse(JObject json)
        {
            var component = (string)json["component"] ?? throw new ArgumentException("The component is not found in json object.");
            var message = (string)json["message"] ?? throw new ArgumentException("The message is not found in json object.");
            var level = (int?)json["level"] ?? throw new ArgumentException("The level is found in json object.");
            var timeout = json["timestamp"] == null ? DateTime.Now : DateTime.Parse(json["timestamp"].ToString());

            if (level < 1 || level > 5)
                throw new ArgumentException("The log level is not supported.");

            return new LogEntry(component, message, timeout, (LogLevel)level);
        }
    }
}
