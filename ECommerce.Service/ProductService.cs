using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.ServiceAbstraction;
using ECommerce.Shared.ProductDtos;
using MapsterMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Service;

public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
{
    public async Task<IEnumerable<BrandDTO>> GetAllBrandsAcync()
    {
        var brands = await unitOfWork.GerRepository<ProductBrand, int>().GetAllAcync();

        return mapper.Map<IEnumerable<BrandDTO>>(brands);
    }

    public async Task<IEnumerable<ProductDTO>> GetAllProductsAcync()
    {
        var products = await unitOfWork.GerRepository<Product, int>().GetAllAcync();

        return mapper.Map<IEnumerable<ProductDTO>>(products);
    }
    public async Task<IEnumerable<TypeDTO>> GetAllTypesAcync()
    {
        var types = await unitOfWork.GerRepository<ProductType, int>().GetAllAcync();
        return mapper.Map<IEnumerable<TypeDTO>>(types);
    }

    public async Task<ProductDTO?> GetByIdAcync(int id)
    {
        var product = await unitOfWork.GerRepository<Product, int>().GetByIdAcync(id);

        if (product is null)
        {
            Console.WriteLine($"Product not found with id: {id}");
            return null;
        }

        return mapper.Map<ProductDTO>(product);
    }

}