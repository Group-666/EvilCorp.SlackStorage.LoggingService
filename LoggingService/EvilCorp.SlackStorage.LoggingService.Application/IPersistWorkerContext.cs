using EvilCorp.SlackStorage.LoggingService.DomainTypes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace EvilCorp.SlackStorage.LoggingService.Application
{
    public interface IPersistWorkerContext
    {
        ConcurrentQueue<LogEntry> QueueOfWork { get; }
    }
}
