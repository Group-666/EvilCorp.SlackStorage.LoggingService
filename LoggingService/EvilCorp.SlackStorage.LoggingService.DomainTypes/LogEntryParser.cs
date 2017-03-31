using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvilCorp.SlackStorage.LoggingService.DomainTypes
{
    public class LogParser
    {
        public static LogEntry Parse(JObject json)
        {
            var component = (string) json["component"] ?? throw new ArgumentException("The component is not found in json object.");
            var message = (string) json["message"] ?? throw new ArgumentException("The message is not found in json object.");
            var level = json["level"] ?? throw new ArgumentException("The level is found in json object.");

            if ((int)level < 1 || (int)level > 5)
                throw new ArgumentException("The log level is not supported.");

            return new LogEntry(component, message, (LogLevel)(int)level);
        }
    }
}
