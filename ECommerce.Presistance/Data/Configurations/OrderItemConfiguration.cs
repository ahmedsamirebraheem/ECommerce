using ECommerce.Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistance.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(x => x.Price).HasPrecision(8, 2);
        builder.OwnsOne(x => x.Product, p =>
        {
            p.Property(x => x.ProductName).HasMaxLength(100);
            p.Property(x => x.PictureUrl).HasMaxLength(200);
        });
    }
}
