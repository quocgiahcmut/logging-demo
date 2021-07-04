using LoggingTest.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoggingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {
        // GET: api/<ValueController>
        [HttpGet]
        public IEnumerable<Value> Get()
        {
            return new Value[] {
                new Value { Name = "111", Id = 1 },
                new Value { Name = "222", Id = 2 }
            };
        }

        // GET api/<ValueController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValueController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValueController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValueController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
