using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//serilog
using Serilog;
using Microsoft.Extensions.Logging;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoggingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private static int _callCount;

        private readonly ILogger<HomeController> _logger;
        private readonly IDiagnosticContext _diagnosticContext;

        public HomeController(ILogger<HomeController> logger, IDiagnosticContext diagnosticContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _diagnosticContext = diagnosticContext ?? throw new ArgumentNullException(nameof(diagnosticContext));
        }

        // GET: api/<HomeController>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Hello, world!");
            _diagnosticContext.Set("IndexCallCount", Interlocked.Increment(ref _callCount));

            return Ok();
        }

        // GET api/<HomeController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 1)
            {
                _logger.LogInformation("Hello, world!");
                _diagnosticContext.Set("IndexCallCount", Interlocked.Increment(ref _callCount));
                return Ok();
            }
            else if (id == 2)
            {
                object o = null;

                try
                {
                    int i = (int)o;
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    _logger.LogError(ex.ToString());
                    _diagnosticContext.Set("IndexCallCount", Interlocked.Increment(ref _callCount));
                    return BadRequest();
                }
            }
            else
            {
                _logger.LogWarning("id not exist");
                _diagnosticContext.Set("IndexCallCount", Interlocked.Increment(ref _callCount));

                return BadRequest();
            }
        }

        // POST api/<HomeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HomeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HomeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
