using EvilCorp.SlackStorage.LoggingService.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace EvilCorp.SlackStorage.LoggingService.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(async () => await new PersistWorker().Run());

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
