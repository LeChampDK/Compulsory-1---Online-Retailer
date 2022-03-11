using CustomerAPI.Models;
using CustomerAPI.Services.Facade;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerAPI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> Get(int id)
        {
            CustomerModel customer = await _customerService.Get(id);

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerModel customer)
        {
            await _customerService.Add(customer);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(CustomerModel customer)
        {
            await _customerService.Update(customer);

            return Ok();
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            await _customerService.Delete(customerId);

            return Ok();
        }
    }
}
