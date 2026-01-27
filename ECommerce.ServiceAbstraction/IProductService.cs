using ECommerce.Shared.ProductDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.ServiceAbstraction;

public interface  IProductService
{
    Task<IEnumerable<ProductDTO>> GetAllProductsAcync();
    Task<ProductDTO?> GetByIdAcync(int id);
    Task<IEnumerable<TypeDTO>> GetAllTypesAcync();
    Task<IEnumerable<BrandDTO>> GetAllBrandsAcync();

}
