using CodeChallengeAPI.Interfaces;
using CodeChallengeAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallengeAPI.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class Customers: ControllerBase
    {
        private ICRUDService _service;

        public Customers(ICRUDService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<List<Models.Customers>> GetCustomers()
        {
            return  await _service.getCustomers();
        }

        [HttpPost]
        public async Task<List<Models.Customers>> CreateCustomer([FromBody] Models.Customers customer)
        {
            return await _service.AddCustomer(customer);
        }

    }
}
