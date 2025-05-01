using Microsoft.AspNetCore.Mvc;
using BlazorApp.Controllers;
using BlazorApp.Repositories.Intefaces;
using BlazorApp.Shared.Models.Entites;
using BlazorApp.Shared.Models.DTO;
using BlazorApp.Shared.Models.Parameters;

namespace BlazorApp.Tests.Components
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly CustomerController _controller;

        public CustomerControllerTests()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _controller = new CustomerController(_mockCustomerRepository.Object);
        }

        [Fact]
        public async Task GetCustomers_ReturnsOkResult_WithListOfCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = "1", CompanyName = "Company1", ContactName = "Name1", Country = "Country1" },
                new Customer { Id = "2", CompanyName = "Company2", ContactName = "Name2", Country = "Country1" }
            };
            _mockCustomerRepository.Setup(repo => repo.GetAllCustomersAsync()).ReturnsAsync(customers);

            // Act
            var result = await _controller.GetCustomers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Customer>>(okResult.Value);
            Assert.Equal(2, ((List<Customer>)returnValue).Count);
        }

        [Fact]
        public async Task GetPaginatedCustomers_ReturnsOkResult_WithPagedCustomers()
        {
            // Arrange
            var parameters = new PaginationParameters { PageNumber = 1, PageSize = 10 };
            var pagedResult = new PagedResultDto<Customer>
            {
                PageNumber = 1,
                PageSize = 10,
                TotalCount = 2,
                TotalPages = 1,
                Items = new List<Customer>
                {
                    new Customer { Id = "1", CompanyName = "Company1", ContactName = "Name1" },
                    new Customer { Id = "2", CompanyName = "Company2", ContactName = "Name2" }
                }
            };

            _mockCustomerRepository.Setup(repo => repo.GetPaginatedCustomersAsync(
                It.Is<PaginationParameters>(p => p.PageNumber == parameters.PageNumber && p.PageSize == parameters.PageSize)))
                .ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.GetPaginatedCustomers(parameters);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PagedResultDto<Customer>>(okResult.Value);
            Assert.Equal(1, returnValue.PageNumber);
            Assert.Equal(10, returnValue.PageSize);
            Assert.Equal(2, returnValue.TotalCount);
            Assert.Equal(2, returnValue.Items.Count());
        }

        [Fact]
        public async Task AddCustomer_ReturnsOkResult_WithNewCustomer()
        {
            // Arrange
            var customer = new Customer
            {
                Id = "3",
                CompanyName = "Company3",
                ContactName = "Name3",
                Address = "Address3",
                City = "City3",
                Region = "Region3",
                PostalCode = "12345",
                Country = "Country3",
                Phone = "123-456-7890"
            };

            _mockCustomerRepository.Setup(repo => repo.AddCustomerAsync(It.IsAny<Customer>()))
                .ReturnsAsync(customer);

            // Act
            var result = await _controller.AddCustomer(customer);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal("3", returnValue.Id);
            Assert.Equal("Company3", returnValue.CompanyName);
            Assert.Equal("Name3", returnValue.ContactName);
        }

        [Fact]
        public async Task AddCustomer_ReturnsBadRequest_WhenRepositoryReturnsNull()
        {
            // Arrange
            var customer = new Customer
            {
                Id = "4",
                CompanyName = "Company4"
            };

            _mockCustomerRepository.Setup(repo => repo.AddCustomerAsync(It.IsAny<Customer>()))
                .ReturnsAsync((Customer)null);

            // Act
            var result = await _controller.AddCustomer(customer);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateCustomer_ReturnsOkResult_WhenCustomerExists()
        {
            // Arrange
            var customer = new Customer
            {
                Id = "5",
                CompanyName = "Company5",
                ContactName = "Name5"
            };

            _mockCustomerRepository.Setup(repo => repo.UpdateCustomerAsync(It.IsAny<Customer>()))
                .ReturnsAsync(customer);

            // Act
            var result = await _controller.UpdateCustomer(customer);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal("5", returnValue.Id);
            Assert.Equal("Company5", returnValue.CompanyName);
        }

        [Fact]
        public async Task UpdateCustomer_ReturnsBadRequest_WhenCustomerDoesNotExist()
        {
            // Arrange
            var customer = new Customer
            {
                Id = "999",
                CompanyName = "NonexistentCompany"
            };

            _mockCustomerRepository.Setup(repo => repo.UpdateCustomerAsync(It.IsAny<Customer>()))
                .ReturnsAsync((Customer)null);

            // Act
            var result = await _controller.UpdateCustomer(customer);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task RemoveCustomer_ReturnsOkResult_WhenCustomerExists()
        {
            // Arrange
            var customerId = "6";
            var customer = new Customer { Id = customerId, CompanyName = "Company6" };

            _mockCustomerRepository.Setup(repo => repo.RemoveCustomerAsync(customerId))
                .ReturnsAsync(customer);

            // Act
            var result = await _controller.RemoveCustomer(customerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal(customerId, returnValue.Id);
        }

        [Fact]
        public async Task RemoveCustomer_ReturnsBadRequest_WhenCustomerDoesNotExist()
        {
            // Arrange
            var customerId = "999";

            _mockCustomerRepository.Setup(repo => repo.RemoveCustomerAsync(customerId))
                .ReturnsAsync((Customer)null);

            // Act
            var result = await _controller.RemoveCustomer(customerId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}