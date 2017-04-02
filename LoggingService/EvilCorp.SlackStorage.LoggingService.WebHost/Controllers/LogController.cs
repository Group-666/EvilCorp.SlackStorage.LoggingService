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
        private readonly IPersistWorkerContext _context;

        public LogController(IPersistWorkerContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post([FromBody]JObject json)
        {
            try
            {
                var log = LogParser.Parse(json);

                _context.QueueOfWork.Enqueue(log);

                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                return BadRequest(ex.Message);
            }
        }
    }
}
