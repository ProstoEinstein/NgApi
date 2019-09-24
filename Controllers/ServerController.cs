using Microsoft.AspNetCore.Mvc;
using NgApi.Models;
using System.Linq;

namespace NgApi.Controllers
{
    [Route("api/[controller]")]
    public class ServerController : Controller
    {
        private readonly ApiContext ctx;

        public ServerController(ApiContext context)
        {
            ctx = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = ctx.Servers.OrderBy(s => s.Id).ToList();
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetServer")]
        public IActionResult Get(int id)
        {
            var response = ctx.Servers.Find(id);
            return Ok(response);
        }


        [HttpPut("{id}")]
        public IActionResult Message(int id, [FromBody] ServerMessage msg)
        {
            var server = ctx.Servers.Find(id);

            if (server == null)
            {
                return NotFound();
            }

            //Refactor: move into a service
            if (msg.PayLoad == "active")
            {
                server.IsOnline = true;
            }
            if (msg.PayLoad == "deactivate")
            {
                server.IsOnline = false;
            }
            ctx.SaveChanges();
            return new NoContentResult();
        }
    }
}