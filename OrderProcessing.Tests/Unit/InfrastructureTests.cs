using Xunit;
using Microsoft.EntityFrameworkCore;
using OrderProcessing.Infrastructure.Persistence;
using OrderProcessing.Infrastructure.Repositories;
using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.ValueObjects;

namespace OrderProcessing.Tests.Unit
{
    public class InfrastructureTests
    {
        private OrderContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<OrderContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new OrderContext(options);
        }

        [Fact]
        public async Task Repository_AddAndGet_Works()
        {
            var context = CreateContext();
            var repo = new OrderRepository(context);
            var order = new Order(new[] { new OrderItem(Guid.NewGuid(), "P", 1, 5m) }, "addr", "a@b.com", "cc");
            await repo.AddAsync(order);
            var fetched = await repo.GetByIdAsync(order.OrderNumber);
            Assert.NotNull(fetched);
            Assert.Equal(order.OrderNumber, fetched.OrderNumber);
        }
    }
}
