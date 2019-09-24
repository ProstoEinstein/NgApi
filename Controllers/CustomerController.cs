using Microsoft.AspNetCore.Mvc;
using NgApi.Models;
using System.Linq;

namespace NgApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ApiContext ctx;

        public CustomerController(ApiContext context)
        {
            ctx = context;
        }

        [HttpGet]
        public IActionResult GetAction()
        {
            var data = ctx.Customers.OrderBy(c => c.Id);
            return Ok(data);
        }

        // GET api/customers/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            var customer = ctx.Customers.Find(id);
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            ctx.Customers.Add(customer);
            ctx.SaveChanges();
            return CreatedAtRoute("GetCustomer", new { id = customer.Id}, customer);
        }
    }
}