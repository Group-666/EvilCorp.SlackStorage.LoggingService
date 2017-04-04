using EvilCorp.SlackStorage.LoggingService.DataAccess;
using MongoDB.Driver;

namespace EvilCorp.SlackStorage.LoggingService.Application
{
    public class PersistWorkerFactory
    {
        public PersistWorker CreateWorker(string connectionString)
            => new PersistWorker(
                PersistWorkerContext.Current, 
                new LogRepository(
                    new MongoClient(connectionString)));
    }
}
