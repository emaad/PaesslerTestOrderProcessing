using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Domain.ValueObjects
{
    public record OrderItem(Guid ProductId, string ProductName, int ProductAmount, decimal ProductPrice)
    {
        public decimal TotalPrice => ProductAmount * ProductPrice;
    }
}