using Microsoft.EntityFrameworkCore;
using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.ValueObjects;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace OrderProcessing.Infrastructure.Persistence;

public class OrderContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(o =>
        {
            o.HasKey(x => x.OrderNumber);
            o.OwnsMany(x => x.Items, ib =>
            {
                ib.WithOwner().HasForeignKey("OrderNumber");
                ib.Property<Guid>("Id");
                ib.HasKey("Id");
                ib.Property(i => i.ProductId);
                ib.Property(i => i.ProductName).HasMaxLength(200);
                ib.Property(i => i.ProductAmount);
                ib.Property(i => i.ProductPrice);
            });
        });
    }
}