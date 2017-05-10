using EvilCorp.SlackStorage.LoggingService.Application;
using EvilCorp.SlackStorage.LoggingService.DomainTypes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EvilCorp.SlackStorage.LoggingService.WebHost.Controllers
{
    [Route("api/[controller]")]
    public class LogController : Controller
    {
        private readonly IPersistWorkerContext _context;
        private readonly LogFacade _facade;

        public LogController(IPersistWorkerContext context, LogFacade facade)
        {
            _context = context;
            _facade = facade;
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

        [HttpGet]
        public async Task<IEnumerable<LogEntry>> Get()
        {
            try
            {
                return await _facade.GetAllEntriesOrdered();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                return new LogEntry[0];
            }
        }
    }
}
