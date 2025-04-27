using Microsoft.EntityFrameworkCore;
using BlazorApp.Repositories.Intefaces;
using BlazorApp.Models;
using BlazorApp.Data;

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
            var result = await _mainContext.Customers.ToListAsync();
            return result;
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            if (customer == null)
            {
                Console.WriteLine("Missing Customer to Add");
                throw new InvalidOperationException();
            }

            var existingCustomer = await _mainContext.Customers.FindAsync(customer.Id);

            if (existingCustomer != null)
            {
                Console.WriteLine("Customer with Id: " + customer.Id + " already exists.");
                throw new InvalidOperationException();
            }

            await _mainContext.Customers.AddAsync(customer);
            await _mainContext.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            var existingCustomer = await _mainContext.Customers.FindAsync(customer.Id);

            if (existingCustomer == null)
            {
                Console.WriteLine("Customer with Id: " + " not exists.");
                throw new InvalidOperationException();
            }

            _mainContext.Entry(existingCustomer).State = EntityState.Detached;
            _mainContext.Customers.Attach(customer);
            _mainContext.Entry(customer).State = EntityState.Modified;

            await _mainContext.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> RemoveCustomerAsync(string id)
        {
            var existingCustomer = await _mainContext.Customers.FindAsync(id);

            if (existingCustomer == null)
            {
                Console.WriteLine("Customer with id: " + " not exists.");
                throw new InvalidOperationException();
            }

            _mainContext.Customers.Remove(existingCustomer);
            await _mainContext.SaveChangesAsync();

            return existingCustomer;
        }
    }
}
