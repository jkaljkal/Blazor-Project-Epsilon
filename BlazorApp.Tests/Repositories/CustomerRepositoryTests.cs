using Microsoft.EntityFrameworkCore;
using BlazorApp.Data;
using BlazorApp.Repositories.Implementations;
using BlazorApp.Shared.Models.Entites;
using BlazorApp.Shared.Models.Parameters;

namespace BlazorApp.Test.Repositories
{
    public class CustomerRepositoryTests
    {
        private DbContextOptions<MainContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<MainContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        private async Task SeedDatabaseAsync(MainContext context)
        {
            await context.Customers.AddRangeAsync(
                new Customer
                {
                    Id = "1",
                    CompanyName = "Company1",
                    ContactName = "Name1",
                    Address = "Address1",
                    City = "City1",
                    Region = null,
                    PostalCode = "12345",
                    Country = "Country1",
                    Phone = "123-456-7890"
                },
                new Customer
                {
                    Id = "2",
                    CompanyName = "Company2",
                    ContactName = "Name2",
                    Address = "Address2",
                    City = "City2",
                    Region = null,
                    PostalCode = "23456",
                    Country = "Country2",
                    Phone = "234-567-8901"
                });

            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetAllCustomersAsync_ReturnsAllCustomers()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new MainContext(options))
            {
                await SeedDatabaseAsync(context);
                var repository = new CustomerRepository(context);

                // Act
                var result = await repository.GetAllCustomersAsync();

                // Assert
                var customers = Assert.IsAssignableFrom<IEnumerable<Customer>>(result);
                Assert.Equal(2, customers.Count());
                Assert.Contains(customers, c => c.Id == "1");
                Assert.Contains(customers, c => c.Id == "2");
            }
        }

        [Fact]
        public async Task GetPaginatedCustomersAsync_ReturnsCorrectPage()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new MainContext(options))
            {
                await SeedDatabaseAsync(context);
                var repository = new CustomerRepository(context);
                var parameters = new PaginationParameters { PageNumber = 1, PageSize = 1 };

                // Act
                var result = await repository.GetPaginatedCustomersAsync(parameters);

                // Assert
                Assert.Equal(1, result.PageNumber);
                Assert.Equal(1, result.PageSize);
                Assert.Equal(2, result.TotalCount);
                Assert.Equal(2, result.TotalPages);
                Assert.Single(result.Items);
                Assert.True(result.HasNext);
                Assert.False(result.HasPrevious);
            }
        }

        [Fact]
        public async Task GetPaginatedCustomersAsync_ReturnsSecondPage()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new MainContext(options))
            {
                await SeedDatabaseAsync(context);
                var repository = new CustomerRepository(context);
                var parameters = new PaginationParameters { PageNumber = 2, PageSize = 1 };

                // Act
                var result = await repository.GetPaginatedCustomersAsync(parameters);

                // Assert
                Assert.Equal(2, result.PageNumber);
                Assert.Equal(1, result.PageSize);
                Assert.Equal(2, result.TotalCount);
                Assert.Equal(2, result.TotalPages);
                Assert.Single(result.Items);
                Assert.False(result.HasNext);
                Assert.True(result.HasPrevious);
            }
        }

        [Fact]
        public async Task AddCustomerAsync_AddsNewCustomer()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new MainContext(options))
            {
                var repository = new CustomerRepository(context);
                var customer = new Customer
                {
                    Id = "3",
                    CompanyName = "Company3",
                    ContactName = "Name3",
                    Address = "Address3",
                    City = "City3",
                    Region = "Region3",
                    PostalCode = "34567",
                    Country = "Country3",
                    Phone = "345-678-9012"
                };

                // Act
                var result = await repository.AddCustomerAsync(customer);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("3", result.Id);

                var savedCustomer = await context.Customers.FindAsync("3");
                Assert.NotNull(savedCustomer);
                Assert.Equal("Company3", savedCustomer.CompanyName);
                Assert.Equal("Name3", savedCustomer.ContactName);
            }
        }

        [Fact]
        public async Task AddCustomerAsync_ThrowsException_WhenCustomerIsNull()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new MainContext(options))
            {
                var repository = new CustomerRepository(context);

                // Act & Assert
                await Assert.ThrowsAsync<InvalidOperationException>(() =>
                    repository.AddCustomerAsync(null));
            }
        }

        [Fact]
        public async Task AddCustomerAsync_ThrowsException_WhenCustomerAlreadyExists()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new MainContext(options))
            {
                await SeedDatabaseAsync(context);
                var repository = new CustomerRepository(context);
                var customer = new Customer
                {
                    Id = "1", // Already exists
                    CompanyName = "Duplicate Company"
                };

                // Act & Assert
                await Assert.ThrowsAsync<InvalidOperationException>(() =>
                    repository.AddCustomerAsync(customer));
            }
        }

        [Fact]
        public async Task UpdateCustomerAsync_UpdatesExistingCustomer()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new MainContext(options))
            {
                await SeedDatabaseAsync(context);
                var repository = new CustomerRepository(context);
                var updatedCustomer = new Customer
                {
                    Id = "1",
                    CompanyName = "Updated Company1",
                    ContactName = "Updated Name1",
                    Address = "Updated Address1",
                    City = "Updated City1",
                    Region = "Updated Region1",
                    PostalCode = "11111",
                    Country = "Updated Country1",
                    Phone = "111-111-1111"
                };

                // Act
                var result = await repository.UpdateCustomerAsync(updatedCustomer);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("1", result.Id);
                Assert.Equal("Updated Company1", result.CompanyName);

                var savedCustomer = await context.Customers.FindAsync("1");
                Assert.NotNull(savedCustomer);
                Assert.Equal("Updated Company1", savedCustomer.CompanyName);
                Assert.Equal("Updated Name1", savedCustomer.ContactName);
            }
        }

        [Fact]
        public async Task UpdateCustomerAsync_ThrowsException_WhenCustomerDoesNotExist()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new MainContext(options))
            {
                await SeedDatabaseAsync(context);
                var repository = new CustomerRepository(context);
                var customer = new Customer
                {
                    Id = "999",
                    CompanyName = "NonexistentCompany"
                };

                // Act & Assert
                await Assert.ThrowsAsync<InvalidOperationException>(() =>
                    repository.UpdateCustomerAsync(customer));
            }
        }

        [Fact]
        public async Task RemoveCustomerAsync_RemovesExistingCustomer()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new MainContext(options))
            {
                await SeedDatabaseAsync(context);
                var repository = new CustomerRepository(context);
                var customerId = "1";

                // Act
                var result = await repository.RemoveCustomerAsync(customerId);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(customerId, result.Id);

                var removedCustomer = await context.Customers.FindAsync(customerId);
                Assert.Null(removedCustomer);

                var remainingCount = await context.Customers.CountAsync();
                Assert.Equal(1, remainingCount);
            }
        }

        [Fact]
        public async Task RemoveCustomerAsync_ThrowsException_WhenCustomerDoesNotExist()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new MainContext(options))
            {
                await SeedDatabaseAsync(context);
                var repository = new CustomerRepository(context);
                var nonExistentId = "999";

                // Act & Assert
                await Assert.ThrowsAsync<InvalidOperationException>(() =>
                    repository.RemoveCustomerAsync(nonExistentId));
            }
        }
    }
}