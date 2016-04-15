using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using NorthwindJson.Data;

namespace NorthwindJson.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly NorthwindSlim _dbContext;

        public CustomersController(NorthwindSlim dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _dbContext.Customers
                .ToListAsync();
            return Ok(customers);
        }

        // GET api/customers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var customers = await _dbContext.Customers
                .SingleOrDefaultAsync(e => e.CustomerId == id);
            return Ok(customers);
        }

        // POST api/customers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Customer customer)
        {
            _dbContext.Entry(customer).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
            return Ok(customer);
        }

        // PUT api/customers/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Customer customer)
        {
            _dbContext.Entry(customer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok(customer);
        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var customer = await _dbContext.Customers
                .SingleOrDefaultAsync(e => e.CustomerId == id);
            if (customer == null) return Ok();

            _dbContext.Entry(customer).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
