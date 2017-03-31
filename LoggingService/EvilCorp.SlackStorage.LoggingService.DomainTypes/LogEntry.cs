using System;
using System.Collections.Generic;
using System.Text;

namespace EvilCorp.SlackStorage.LoggingService.DomainTypes
{
    public class LogEntry
    {
        public LogLevel Type { get; } 
        public string Component { get; }
        public string Message { get; }

        public LogEntry(string component, string message, LogLevel type)
        {
            if (string.IsNullOrEmpty(component))
                throw new ArgumentException("The component cannot be null or empty.", nameof(component));
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("The message cannot be null or empty.", nameof(message));

            Component = component;
            Message = message;
            Type = type;
        }
    }
}
