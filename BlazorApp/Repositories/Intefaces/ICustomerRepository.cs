using BlazorApp.Models;

namespace BlazorApp.Repositories.Intefaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        Task<Customer> AddCustomerAsync(Customer customer);

        Task<Customer> UpdateCustomerAsync(Customer customer);

        Task<Customer> RemoveCustomerAsync(string id);
    }
}
