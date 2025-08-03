using MediatR;
using OrderProcessing.Application.DTOs;
using OrderProcessing.Application.Queries;
using OrderProcessing.Domain.Interfaces;

namespace OrderProcessing.Application.Queries;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly IOrderRepository _repo;
    public GetOrderByIdQueryHandler(IOrderRepository repo) => _repo = repo;

    public async Task<OrderDto?> Handle(GetOrderByIdQuery qry, CancellationToken ct)
    {
        var order = await _repo.GetByIdAsync(qry.OrderNumber);
        if (order == null) return null;
        var items = order.Items.Select(i => new OrderItemDto(i.ProductId, i.ProductName, i.ProductAmount, i.ProductPrice)).ToList();
        return new OrderDto(order.OrderNumber, items, order.InvoiceAddress, order.InvoiceEmail, order.InvoiceCreditCard, order.CreatedAt);
    }
}