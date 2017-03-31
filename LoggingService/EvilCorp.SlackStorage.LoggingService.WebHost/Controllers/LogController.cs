using EvilCorp.SlackStorage.LoggingService.Application;
using EvilCorp.SlackStorage.LoggingService.DomainTypes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace EvilCorp.SlackStorage.LoggingService.WebHost.Controllers
{
    [Route("api/[controller]")]
    public class LogController : Controller
    {
        [HttpPost]
        public void Post([FromBody]JObject json)
        {
            try
            {
                var log = LogParser.Parse(json);

                PersistWorkerContext.Current.QueueOfWork.Enqueue(log);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
