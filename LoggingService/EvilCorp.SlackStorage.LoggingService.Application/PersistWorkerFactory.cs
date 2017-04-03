using EvilCorp.SlackStorage.LoggingService.DataAccess;

namespace EvilCorp.SlackStorage.LoggingService.Application
{
    public class PersistWorkerFactory
    {
        public PersistWorker CreateWorker(string connectionString)
            => new PersistWorker(
                PersistWorkerContext.Current, 
                new LogRepository(connectionString));
    }
}
