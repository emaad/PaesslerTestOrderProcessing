using Xunit;
using Moq;
using OrderProcessing.Application.Commands;
using OrderProcessing.Application.Queries;
using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.ValueObjects;
using OrderProcessing.Domain.Interfaces;
using OrderProcessing.Application.DTOs;

namespace OrderProcessing.Tests.Unit
{
    public class ApplicationTests
    {
        [Fact]
        public async Task CreateOrderCommandHandler_CreatesOrder_ReturnsGuid()
        {
            var mockRepo = new Mock<IOrderRepository>();
            var handler = new CreateOrderCommandHandler(mockRepo.Object);
            var items = new List<OrderItemDto> { new(Guid.NewGuid(), "P", 1, 5m) };
            var cmd = new CreateOrderCommand(items, "addr", "a@b.com", "cc");

            var result = await handler.Handle(cmd, CancellationToken.None);

            mockRepo.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Once);
            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task GetOrderByIdQueryHandler_ReturnsDto_WhenExists()
        {
            var order = new Order(new[] { new OrderItem(Guid.NewGuid(), "P", 1, 5m) }, "addr", "a@b.com", "cc");
            var mockRepo = new Mock<IOrderRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(order.OrderNumber)).ReturnsAsync(order);
            var handler = new GetOrderByIdQueryHandler(mockRepo.Object);

            var dto = await handler.Handle(new GetOrderByIdQuery(order.OrderNumber), CancellationToken.None);

            Assert.NotNull(dto);
            Assert.Equal(order.OrderNumber, dto.OrderNumber);
        }
    }
}
