using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities.ProductModule;

public class Product: BaseEntity<int>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PictureUrl { get; set; } = null!;
    public decimal Price { get; set; }
    public int BrandId { get; set; }
    public ProductBrand ProductBrand { get; set; } = null!;
    public int TypeId { get; set; }
    public ProductType ProductType { get; set; } = null!;

}
