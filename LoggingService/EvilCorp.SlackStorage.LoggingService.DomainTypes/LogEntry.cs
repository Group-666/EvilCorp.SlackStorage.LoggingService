using System;

namespace EvilCorp.SlackStorage.LoggingService.DomainTypes
{
    public class LogEntry
    {
        public string Component { get; }
        public string Message { get; }
        public DateTime Timestamp { get; }
        public LogLevel Level { get; }

        public LogEntry(string component, string message, DateTime timestamp, LogLevel level)
        {
            if (string.IsNullOrEmpty(component))
                throw new ArgumentException("The component cannot be null or empty.", nameof(component));
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("The message cannot be null or empty.", nameof(message));

            Component = component;
            Message = message;
            Timestamp = timestamp;
            Level = level;
        }
    }
}
