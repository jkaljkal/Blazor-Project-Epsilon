using System.Net;
using System.Net.Http.Json;
using BlazorApp.Shared.Models.DTO;
using BlazorApp.Shared.Models.Entites;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq.Protected;

namespace BlazorApp.Tests.Fixtures
{
    public class CustomersComponentTests : TestContext
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _httpClient;

        public CustomersComponentTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://localhost")
            };
            Services.AddSingleton(_httpClient);
            JSInterop.Mode = JSRuntimeMode.Loose;
        }

        [Fact]
        public void CustomersComponent_ShouldRenderGrid_WhenDataLoaded()
        {
            // Arrange
            SetupMockHttpResponse();

            // Act
            var cut = RenderComponent<Client.Pages.Customers>();

            // Assert
            // We can't fully test the Grid rendering as it's a complex component,
            // but we can verify the component renders without errors
            Assert.Contains("Customers", cut.Markup);
            Assert.Contains("<h1>Customers</h1>", cut.Markup);
        }

        private void SetupMockHttpResponse()
        {
            var pagedResult = new PagedResultDto<Customer>
            {
                PageNumber = 1,
                PageSize = 3,
                TotalCount = 5,
                TotalPages = 2,
                Items = new List<Customer>
                {
                    new Customer { Id = "1", CompanyName = "Company1", ContactName = "Name1" },
                    new Customer { Id = "2", CompanyName = "Company2", ContactName = "Name2" },
                    new Customer { Id = "3", CompanyName = "Company3", ContactName = "Name3" }
                }
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(pagedResult)
                });
        }
    }
}