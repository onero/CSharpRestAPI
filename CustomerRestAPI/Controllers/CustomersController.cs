using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestAppBLL;
using RestAppBLL.BusinessObjects;

namespace CustomerRestAPI.Controllers
{
    [EnableCors("LocalPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly BLLFacade _facade = new BLLFacade();

        // GET: api/Customers
        [HttpGet]
        public IEnumerable<CustomerBO> Get()
        {
            return _facade.CustomerService.GetAll();
        }

        // GET: api/Customers/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            var customer = _facade.CustomerService.GetById(id);

            return customer == null ? 
                NotFound($"ID: {id} - does not exist") : 
                new ObjectResult(customer);
        }

        // POST: api/Customers
        [HttpPost]
        public IActionResult Post([FromBody] CustomerBO customer)
        {
            if (customer == null) return BadRequest("JSON Object is not valid");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Created("", _facade.CustomerService.Create(customer));
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CustomerBO customer)
        {
            if (id != customer.Id) return BadRequest($"ID: {id} - does not match customer ID");

            var customerFromDB = _facade.CustomerService.GetById(id);
            if (customerFromDB == null) return NotFound();

            return Ok(_facade.CustomerService.Update(customer));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _facade.CustomerService.GetById(id);

            if (customer == null) return NotFound($"ID: {id} - does not exist");

            return Ok(_facade.CustomerService.Delete(id));
        }
    }
}