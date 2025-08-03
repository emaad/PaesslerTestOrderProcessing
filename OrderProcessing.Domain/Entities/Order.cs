using OrderProcessing.Domain.ValueObjects;
using OrderProcessing.Domain.Exceptions;

namespace OrderProcessing.Domain.Entities
{
    public class Order
    {
        public Guid OrderNumber { get; private set; }
        public List<OrderItem> Items { get; private set; } = new();
        public string InvoiceAddress { get; private set; }
        public string InvoiceEmail { get; private set; }
        public string InvoiceCreditCard { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Order() { }

        public Order(IEnumerable<OrderItem> items, string address, string email, string creditCard)
        {
            if (!items.Any()) throw new DomainException("Order must contain at least one item");
            if (!email.Contains("@")) throw new DomainException("Invalid email address");

            OrderNumber = Guid.NewGuid();
            Items = items.ToList();
            InvoiceAddress = address;
            InvoiceEmail = email;
            InvoiceCreditCard = creditCard;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
