using BlazorApp.Data;
using BlazorApp.Shared.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Test.Fixtures
{
    public class CustomerDatabaseFixture : IDisposable
    {
        public MainContext Context { get; private set; }

        public CustomerDatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<MainContext>()
                .UseInMemoryDatabase(databaseName: "CustomerTestDb_" + Guid.NewGuid().ToString())
                .Options;
            Context = new MainContext(options);
            Context.Database.EnsureCreated();
        }

        public async Task SeedDatabaseAsync()
        {
            // Clear existing data
            Context.Customers.RemoveRange(Context.Customers);
            await Context.SaveChangesAsync();

            // Add test data
            await Context.Customers.AddRangeAsync(
                new Customer
                {
                    Id = "1",
                    CompanyName = "Company1",
                    ContactName = "Name1",
                    Address = "Address1",
                    City = "City1",
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
                    PostalCode = "23456",
                    Country = "Country2",
                    Phone = "234-567-8901"
                });

            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}