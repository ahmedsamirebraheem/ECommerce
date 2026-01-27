using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared.ProductDtos;

public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string PictureUrl { get; set; } = null!;
    public string ProductType { get; set; } = null!;
    public string ProductBrand { get; set; } = null!;
}
