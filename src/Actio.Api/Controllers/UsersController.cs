using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Commands.CommandImpl;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Actio.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private IBusClient _busClient;
        public UsersController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] CreateUser command)
        {     
            await _busClient.PublishAsync(command);
            return Accepted();
        }
    }
}