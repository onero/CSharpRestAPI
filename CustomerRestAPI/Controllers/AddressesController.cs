using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAppBLL;
using RestAppBLL.BusinessObjects;
using RestAppBLL.Services;

namespace CustomerRestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Addresses")]
    public class AddressesController : Controller
    {
        private readonly IService<AddressBO> _addressService;

        public AddressesController()
        {
            var facade = new BLLFacade();
            _addressService = facade.AddressService;
        }
        // GET: api/Addresses
        [HttpGet]
        public IEnumerable<AddressBO> Get()
        {
            return _addressService.GetAll();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_addressService.GetById(id));
        }
        
        // POST: api/Addresses
        [HttpPost]
        public IActionResult Post([FromBody]AddressBO address)
        {
            return new ObjectResult(_addressService.Create(address));
        }
        
        // PUT: api/Addresses/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]AddressBO address)
        {
            return new OkObjectResult(_addressService.Update(address));
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _addressService.Delete(id);
            return new OkResult();
        }
    }
}
