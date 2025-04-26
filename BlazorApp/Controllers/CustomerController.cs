using Microsoft.AspNetCore.Mvc;
using BlazorApp.Repositories.Implementations;

namespace BlazorApp.Controllers
{
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IActionResult> GetCustomers()
        {
            var result = await _customerRepository.GetAllCustomersAsync();
            return Ok(result);
        }
    }
}
