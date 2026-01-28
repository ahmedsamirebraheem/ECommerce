using ECommerce.ServiceAbstraction;
using ECommerce.Shared.BasketDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketController(IBasketService basketservice) : ControllerBase
{
    //Get: Baseurl/api/basket?1d
    [HttpGet]
    public async Task<ActionResult<BasketDTO>> GetBasket(string id)
    {
        var basket = await basketservice.GetBasketAsync(id);
        return Ok(basket);
    }
    //Post: Baseurl/api/basket
    [HttpPost]
    public async Task<ActionResult<BasketDTO>> CreateOrUpdateBasket(BasketDTO basket)
    {
        var Basket = await basketservice.CreateOrUpdateBasketAsync(basket);
        return Ok(Basket);
    }

    //Delete: Baseurl/api/basket/id
    [HttpDelete("id")]
    public async Task<ActionResult<bool>> DeleteBasket(string id)
    {
        var result = await basketservice.DeleteBasketAsync(id);
        return Ok(result);
    }

}
