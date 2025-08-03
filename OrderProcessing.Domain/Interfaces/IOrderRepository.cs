using OrderProcessing.Domain.Entities;

namespace OrderProcessing.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order?> GetByIdAsync(Guid orderNumber);
    }
}
