using EvilCorp.SlackStorage.LoggingService.DomainTypes;
using System;
using System.Collections.Concurrent;

namespace EvilCorp.SlackStorage.LoggingService.Application
{
    public class PersistWorkerContext
    {
        public static PersistWorkerContext Current { get; } = new PersistWorkerContext();

        public ConcurrentQueue<LogEntry> QueueOfWork { get; } = new ConcurrentQueue<LogEntry>();

        private PersistWorkerContext()
        { }
    }
}
