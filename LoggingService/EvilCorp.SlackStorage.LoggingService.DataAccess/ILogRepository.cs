using EvilCorp.SlackStorage.LoggingService.DomainTypes;
using System.Threading.Tasks;

namespace EvilCorp.SlackStorage.LoggingService.DataAccess
{
    public interface ILogRepository
    {
        Task Add(LogEntry log);
    }
}
