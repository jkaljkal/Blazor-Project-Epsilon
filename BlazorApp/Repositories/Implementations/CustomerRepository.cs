using BlazorApp.Repositories.Intefaces;
using BlazorApp.Models;
using BlazorApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MainContext _mainContext;

        public CustomerRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _mainContext.Customers.ToListAsync();
        }
    }
}
