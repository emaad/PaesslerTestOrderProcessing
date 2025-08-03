using MediatR;
using OrderProcessing.Application.DTOs;

namespace OrderProcessing.Application.Queries;

public record GetOrderByIdQuery(Guid OrderNumber) : IRequest<OrderDto?>;