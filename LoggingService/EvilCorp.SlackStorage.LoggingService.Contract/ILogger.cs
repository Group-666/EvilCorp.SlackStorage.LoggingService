using System;

namespace EvilCorp.SlackStorage.LoggingService.Contract
{
    public interface ILogger
    {
        void Log(string message, LogLevel level);
    }
}
