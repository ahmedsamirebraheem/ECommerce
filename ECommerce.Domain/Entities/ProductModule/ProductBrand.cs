using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities.ProductModule;

public class ProductBrand : BaseEntity<int> 
{
    public string Name { get; set; } = null!;
}
