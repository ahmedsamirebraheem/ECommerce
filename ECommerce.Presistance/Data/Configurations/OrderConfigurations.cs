using ECommerce.Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistance.Data.Configurations;

public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.Subtotal).HasPrecision(8, 2);
        builder.OwnsOne(x => x.OrderAddress, OEntity =>
        {
            OEntity.Property(x => x.FirstName).HasMaxLength(50);
            OEntity.Property(x => x.LastName).HasMaxLength(50);
            OEntity.Property(x => x.City).HasMaxLength(50);
            OEntity.Property(x => x.Street).HasMaxLength(50);
            OEntity.Property(x => x.Country).HasMaxLength(50);
        });
    }
}
