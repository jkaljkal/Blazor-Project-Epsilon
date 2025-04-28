using BlazorApp.Models;
using BlazorApp.Models.DTO;

namespace BlazorApp.Repositories.Intefaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        Task<Customer> AddCustomerAsync(Customer customer);

        Task<Customer> UpdateCustomerAsync(Customer customer);

        Task<Customer> RemoveCustomerAsync(string id);

        Task<PagedResultDto<Customer>> GetPaginatedCustomersAsync(PaginationParameters parameters);
    }
}
