using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderProcessing.Application.Commands;
using OrderProcessing.Application.Queries;
using OrderProcessing.Domain.Exceptions;

namespace OrderProcessing.API.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrdersController(IMediator mediator) => _mediator = mediator;

    // POST /orders
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand cmd)
    {
        try
        {
            // Dispatch command; FluentValidation runs first
            var orderNumber = await _mediator.Send(cmd);
            // On success, return 201 Created with the orderNumber
            return CreatedAtAction(nameof(Get), new { orderNumber }, new { orderNumber });
        }
        catch (ValidationException vex)
        {
            // Bad request for validation errors (e.g. invalid email)
            var errors = vex.Errors.Select(e => e.ErrorMessage);
            return BadRequest(new { errors });
        }
        catch (DomainException dex)
        {
            // Bad request for out-of-stock business rule
            return BadRequest(new { error = dex.Message });
        }
    }

    [HttpGet("{orderNumber}")]
    public async Task<IActionResult> Get(Guid orderNumber)
    {
        var dto = await _mediator.Send(new GetOrderByIdQuery(orderNumber));
        if (dto == null) return NotFound();
        return Ok(dto);
    }
}