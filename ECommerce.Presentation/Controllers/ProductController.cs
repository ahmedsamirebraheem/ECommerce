using ECommerce.Presentation.Attributes;
using ECommerce.ServiceAbstraction;
using ECommerce.Shared;
using ECommerce.Shared.ProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Controllers;

public class ProductController(IProductService productService) : ApiBaseController
{
    [HttpGet]
    [RedisCache]
    [Authorize]
    //Get: BaseUrl/api/products
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
    {
        var products = await productService.GetAllProductsAcync(queryParams);
        return Ok(products);
    }
    [HttpGet("{id}")]
    //Get: BaseUrl/api/products/id
    public async Task<ActionResult<ProductDTO>> GetProductById(int id)
    {
        var result = await productService.GetByIdAcync(id);
        return HandleResult<ProductDTO>(result);
    }

    [HttpGet("brands")]
    //Get: BaseUrl/api/products/brands
    public async Task<ActionResult<IEnumerable<BrandDTO>>> GetBrands()
    {
        var brands = await productService.GetAllBrandsAcync();
        return Ok(brands);
    }
    [HttpGet("types")]
    //Get: BaseUrl/api/products/types
    public async Task<ActionResult<IEnumerable<TypeDTO>>> GetTypes()
    {
        var types = await productService.GetAllTypesAcync();
        return Ok(types);
    }


}
