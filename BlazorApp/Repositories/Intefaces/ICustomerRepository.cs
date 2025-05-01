using BlazorApp.Shared.Models.Entites;
using BlazorApp.Shared.Models.DTO;
using BlazorApp.Shared.Models.Parameters;

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
