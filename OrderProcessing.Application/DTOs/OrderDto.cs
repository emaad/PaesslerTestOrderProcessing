using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Application.DTOs
{
    public record OrderDto(Guid OrderNumber, List<OrderItemDto> Items, string InvoiceAddress, string InvoiceEmail, string InvoiceCreditCard, DateTime CreatedAt);
}
