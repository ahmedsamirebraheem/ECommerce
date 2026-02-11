using ECommerce.Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistance.Data.Configurations;

public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.Property(x => x.Price).HasPrecision(8, 2);
        builder.Property(x => x.ShortName).HasMaxLength(50);
        builder.Property(x => x.DeliveryTime).HasMaxLength(50);
        builder.Property(x => x.Description).HasMaxLength(50);
    }
}
