using MediatR;
using OrderProcessing.Application.DTOs;

namespace OrderProcessing.Application.Commands;

public record CreateOrderCommand(List<OrderItemDto> Items, string InvoiceAddress, string InvoiceEmail, string InvoiceCreditCard) : IRequest<Guid>;
