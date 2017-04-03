using EvilCorp.SlackStorage.LoggingService.DomainTypes;
using System.Collections.Concurrent;

namespace EvilCorp.SlackStorage.LoggingService.Application
{
    public interface IPersistWorkerContext
    {
        ConcurrentQueue<LogEntry> QueueOfWork { get; }
    }
}
