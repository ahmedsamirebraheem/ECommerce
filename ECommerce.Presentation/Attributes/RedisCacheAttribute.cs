using ECommerce.ServiceAbstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Attributes;

public class RedisCacheAttribute : ActionFilterAttribute
{
    private readonly int _durationInMin;
    public RedisCacheAttribute(int durationInMin=5)
    {
            _durationInMin = durationInMin;
    }
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //get cache service from dependency injection container
        var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
        //create cache key based on request path and query string
        var cacheKey = CreateCacheKey(context.HttpContext.Request);
        //check if cached data exists or not
        var cacheValue = await cacheService.GetAsync(cacheKey);
        if(cacheValue is not null)
        {
            context.Result = new ContentResult
            {
                Content = cacheValue,
                ContentType = "application/json",
                StatusCode = StatusCodes.Status200OK
            };
            return;
        }
        //if exists return cached data and skip executing end point
        //if not exists execute end point then cache the data if 200 ok
        var ExecutedContext = await next.Invoke();
        if(ExecutedContext.Result is OkObjectResult result)
        {
            await cacheService.SetAsync(cacheKey, result.Value!, TimeSpan.FromMinutes(_durationInMin));
        }
        //return base.OnActionExecutionAsync(context, next);
    }
    private string CreateCacheKey(HttpRequest request)
    {
        StringBuilder key = new StringBuilder();
        key.Append(request.Path);
        foreach (var item in request.Query.OrderBy(x=>x.Key))
        {
            key.Append($"{ item.Key}-{item.Value}");
        }
        return key.ToString();
    }
     
}
