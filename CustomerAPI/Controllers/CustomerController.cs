using CustomerAPI.Models;
using CustomerAPI.Services.Facade;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("Get/Customer")]
        public async Task<IActionResult> GetCustomer(int customerId)
        {
            CustomerModel customer = await _customerService.Get(customerId);

            return Ok(customer);
        }

        [HttpPost("Add/Customer")]
        public async Task<IActionResult> AddCustomer(CustomerModel customer)
        {
            await _customerService.Add(customer);

            return Ok();
        }

        [HttpPut("Update/Customer")]
        public async Task<IActionResult> UpdateCustomer(CustomerModel customer)
        {
            await _customerService.Update(customer);

            return Ok();
        }

        [HttpDelete("Delete/Customer")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            await _customerService.Delete(customerId);

            return Ok();
        }
    }
}
