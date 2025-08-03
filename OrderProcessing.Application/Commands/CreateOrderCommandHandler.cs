using MediatR;
using OrderProcessing.Application.Commands;
using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.ValueObjects;
using OrderProcessing.Domain.Interfaces;

namespace OrderProcessing.Application.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _repo;
    public CreateOrderCommandHandler(IOrderRepository repo) => _repo = repo;

    public async Task<Guid> Handle(CreateOrderCommand cmd, CancellationToken ct)
    {
        var items = cmd.Items.Select(i => new OrderItem(i.ProductId, i.ProductName, i.ProductAmount, i.ProductPrice));
        var order = new Order(items, cmd.InvoiceAddress, cmd.InvoiceEmail, cmd.InvoiceCreditCard);
        await _repo.AddAsync(order);
        return order.OrderNumber;
    }
}