using BlazorApp.Models;

namespace BlazorApp.Repositories.Intefaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
    }
}
