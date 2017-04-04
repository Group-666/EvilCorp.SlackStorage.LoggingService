using EvilCorp.SlackStorage.LoggingService.DataAccess;
using EvilCorp.SlackStorage.LoggingService.DomainTypes;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace EvilCorp.SlackStorage.LoggingService.Application
{
    public class PersistWorker
    {
        private readonly IPersistWorkerContext _context;
        private readonly ILogRepository _repository;

        public  PersistWorker(IPersistWorkerContext context, ILogRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task Run(CancellationToken token)
        {
            while(!token.IsCancellationRequested)
            {
                await ProcessQueue();

                await Task.Delay(new TimeSpan(0, 0, 1));
            }
        }

        private async Task ProcessQueue()
        {
            while (_context.QueueOfWork.TryDequeue(out LogEntry log))
            {
                try
                {
                    await _repository.Add(log);
                }
                catch (InvalidProgramException exception)
                {
                    Debug.WriteLine(exception);
                }
            }
        }
    }
}
