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

        // Map owned Address value object to existing OrderAddress_* columns
        builder.OwnsOne(x => x.Address, owned =>
        {
            owned.Property(a => a.FirstName)
                 .HasMaxLength(50)
                 .HasColumnName("OrderAddress_FirstName");

            owned.Property(a => a.LastName)
                 .HasMaxLength(50)
                 .HasColumnName("OrderAddress_LastName");

            owned.Property(a => a.City)
                 .HasMaxLength(50)
                 .HasColumnName("OrderAddress_City");

            owned.Property(a => a.Street)
                 .HasMaxLength(50)
                 .HasColumnName("OrderAddress_Street");

            owned.Property(a => a.Country)
                 .HasMaxLength(50)
                 .HasColumnName("OrderAddress_Country");
        });
    }
}
