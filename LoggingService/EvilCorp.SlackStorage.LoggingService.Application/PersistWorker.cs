using EvilCorp.SlackStorage.LoggingService.DataAccess;
using EvilCorp.SlackStorage.LoggingService.DomainTypes;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EvilCorp.SlackStorage.LoggingService.Application
{
    public class PersistWorker
    {
        private readonly LogRepository _repository;

        public  PersistWorker()
        {
            _repository = new LogRepository();
        }

        public async Task Run()
        {
            while(true)
            {
                await ProcessQueue(PersistWorkerContext.Current.QueueOfWork);

                await Task.Delay(new TimeSpan(0, 0, 1));
            }
        }

        private async Task ProcessQueue(ConcurrentQueue<LogEntry> queueOfWork)
        {
            while (queueOfWork.TryDequeue(out LogEntry log))
            {
                try
                {
                    await _repository.Add(log);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
    }
}
