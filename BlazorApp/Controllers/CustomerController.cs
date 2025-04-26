using Microsoft.AspNetCore.Mvc;
using BlazorApp.Repositories.Implementations;
using BlazorApp.Models;

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

        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            var result = await _customerRepository.AddCustomerAsync(customer);
            return result != null ? Ok(result) : BadRequest();
        }

        public async Task<IActionResult> UpdateCustomer(Customer customer)
        {
            var result = await _customerRepository.UpdateCustomerAsync(customer);
            return result != null ? Ok(result) : BadRequest();
        }

        public async Task<IActionResult> RemoveCustomer(int id)
        {
            var result = await _customerRepository.RemoveCustomerAsync(id);
            return result != null ? Ok(result) : BadRequest();
        }
    }
}
