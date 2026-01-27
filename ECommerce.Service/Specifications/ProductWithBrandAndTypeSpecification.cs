using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Service.Specifications;

public class ProductWithBrandAndTypeSpecification : BaseSpecifications<Product, int>
{
    // 1. هنا نستخدم base() الفاضي بدل ما نبعت null
    public ProductWithBrandAndTypeSpecification(ProductQueryParams queryParams)
        : base(p => (queryParams.BrandId == null || p.BrandId == queryParams.BrandId) &&
                                    (queryParams.TypeId == null || p.TypeId == queryParams.TypeId) &&
                                    (string.IsNullOrEmpty(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower())))
    {
        AddIncludes();
        switch (queryParams.Sort)
        {
            case ProductSortingOptions.NameAsc:
                AddOrderBy(p => p.Name);
                break;
            case ProductSortingOptions.NameDesc:
                AddOrderByDescending(p => p.Name);
                break;
            case ProductSortingOptions.PriceAsc:
                AddOrderBy(p => p.Price);
                break;
            case ProductSortingOptions.PriceDesc:
                AddOrderByDescending(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Id);
                break;
        }
        ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
    }

    // 2. هنا بنبعت الـ Criteria للـ base بشكل طبيعي
    public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
    {
        AddIncludes();
    }

    // ميثود صغيرة عشان ما نكررش الـ Includes مرتين (Clean Code)
    private void AddIncludes()
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
    }
}
