using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAppBLL;
using RestAppBLL.BusinessObjects;

namespace CustomerRestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly BLLFacade _facade = new BLLFacade();
        // GET: api/Orders
        [HttpGet]
        public IEnumerable<OrderBO> Get()
        {
            return _facade.OrderService.GetAll();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = _facade.OrderService.GetById(id);

            return customer == null ? 
                NotFound($"ID: {id} - does not exist") : 
                new ObjectResult(customer);
        }
        
        // POST: api/Orders
        [HttpPost]
        public IActionResult Post([FromBody]OrderBO order)
        {
            if (order == null) return BadRequest("JSON Object is not valid");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Created("", _facade.OrderService.Create(order));
        }
        
        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]OrderBO order)
        {
            if (id != order.Id) return BadRequest($"ID: {id} - does not match order ID");

            var orderFromDB = _facade.OrderService.GetById(id);
            if (orderFromDB == null) return NotFound();

            return Ok(_facade.OrderService.Update(order));
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _facade.OrderService.GetById(id);

            if (order == null) return NotFound($"ID: {id} - does not exist");

            return Ok(_facade.OrderService.Delete(id));
        }
    }
}
