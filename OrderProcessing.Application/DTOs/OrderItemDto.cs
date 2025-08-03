using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Application.DTOs
{
    public record OrderItemDto(Guid ProductId, string ProductName, int ProductAmount, decimal ProductPrice);
}
