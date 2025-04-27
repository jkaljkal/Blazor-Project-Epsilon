using Microsoft.AspNetCore.Mvc;
using BlazorApp.Repositories.Intefaces;
using BlazorApp.Models;

namespace BlazorApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await _customerRepository.GetAllCustomersAsync();
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            var result = await _customerRepository.AddCustomerAsync(customer);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            var result = await _customerRepository.UpdateCustomerAsync(customer);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveCustomer(string id)
        {
            var result = await _customerRepository.RemoveCustomerAsync(id);
            return result != null ? Ok(result) : BadRequest();
        }
    }
}
