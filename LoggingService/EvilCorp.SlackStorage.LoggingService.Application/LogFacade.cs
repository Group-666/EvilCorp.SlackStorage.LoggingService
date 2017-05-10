using EvilCorp.SlackStorage.LoggingService.DataAccess;
using EvilCorp.SlackStorage.LoggingService.DomainTypes;
using MongoDB.Driver;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvilCorp.SlackStorage.LoggingService.Application
{
    public class LogFacade
    {
        private readonly LogRepository _repository;

        public LogFacade(string connectionString)
        {
            _repository = new LogRepository(new MongoClient(connectionString));
        }

        public async Task<IEnumerable<LogEntry>> GetAllEntriesOrdered()
        {
            return (await _repository.GetAll()).OrderBy(l => l.Timestamp);
        }
    }
}
