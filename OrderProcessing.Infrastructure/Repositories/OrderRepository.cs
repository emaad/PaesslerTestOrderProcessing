using Microsoft.EntityFrameworkCore;
using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.Interfaces;
using OrderProcessing.Infrastructure.Persistence;

namespace OrderProcessing.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderContext _context;
    public OrderRepository(OrderContext context) => _context = context;

    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid orderNumber)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(x => x.OrderNumber == orderNumber);
    }
}