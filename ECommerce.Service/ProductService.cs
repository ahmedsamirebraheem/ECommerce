using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Service.Exceptions;
using ECommerce.Service.Specifications;
using ECommerce.ServiceAbstraction;
using ECommerce.Shared;
using ECommerce.Shared.Common_Result;
using ECommerce.Shared.ProductDtos;
using MapsterMapper;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ECommerce.Service;

public class ProductService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IPictureUrlResolver pictureUrlResolver) : IProductService
{
    public async Task<IEnumerable<ProductDTO>> GetAllProductsAcync(ProductQueryParams queryParams)
    {
        var spec = new ProductWithBrandAndTypeSpecification(queryParams);
        var products = await unitOfWork.GerRepository<Product, int>().GetAllAcync(spec);

        var productDtos = mapper.Map<IEnumerable<ProductDTO>>(products);

        // هنا بنصلح روابط الصور لكل المنتجات
        foreach (var dto in productDtos)
        {
            dto.PictureUrl = pictureUrlResolver.Resolve(dto.PictureUrl);
        }

        return productDtos;
    }

    public async Task<Result<ProductDTO>> GetByIdAcync(int id)
    {
        var spec = new ProductWithBrandAndTypeSpecification(id);
        var product = await unitOfWork.GerRepository<Product, int>().GetByIdAsync(spec);
        if(product == null)
        return Error.NotFound("Product not found",$"Product with id:{id} not found");
        
        var dto = mapper.Map<ProductDTO>(product);

        // هنا بنصلح رابط الصورة للمنتج ده بس
        dto.PictureUrl = pictureUrlResolver.Resolve(dto.PictureUrl);

        return dto;
    }

    public async Task<IEnumerable<BrandDTO>> GetAllBrandsAcync()
    {
        var brands = await unitOfWork.GerRepository<ProductBrand, int>().GetAllAcync();
        return mapper.Map<IEnumerable<BrandDTO>>(brands);
    }

    public async Task<IEnumerable<TypeDTO>> GetAllTypesAcync()
    {
        var types = await unitOfWork.GerRepository<ProductType, int>().GetAllAcync();
        return mapper.Map<IEnumerable<TypeDTO>>(types);
    }
}