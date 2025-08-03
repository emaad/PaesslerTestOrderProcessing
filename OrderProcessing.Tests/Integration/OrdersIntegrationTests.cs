using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using OrderProcessing.Application.DTOs;
using OrderProcessing.Application.Commands;

namespace OrderProcessing.Tests.Integration
{
    public class OrdersIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public OrdersIntegrationTests(WebApplicationFactory<Program> factory) => _client = factory.CreateClient();

        [Fact]
        public async Task PostAndGetOrder_EndToEnd()
        {
            var command = new CreateOrderCommand(
                            new List<OrderItemDto> { new(Guid.NewGuid(), "Product", 1, 9.99m) },
                            "Address",
                            "email@example.com",
                            "1234-5678-9012-3456"
                        );
            var postResp = await _client.PostAsJsonAsync("/orders", command);
            postResp.EnsureSuccessStatusCode();
            var created = await postResp.Content.ReadFromJsonAsync<CreateResponse>();

            var getResp = await _client.GetAsync($"/orders/{created.OrderNumber}");
            getResp.EnsureSuccessStatusCode();
            var dto = await getResp.Content.ReadFromJsonAsync<OrderDto>();
            Assert.Equal(created.OrderNumber, dto.OrderNumber);
        }

        private record CreateResponse(Guid OrderNumber);
    }
}
