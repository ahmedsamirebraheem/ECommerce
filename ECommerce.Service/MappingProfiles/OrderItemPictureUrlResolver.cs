using ECommerce.Domain.Entities.OrderModule;
using ECommerce.ServiceAbstraction;
using ECommerce.Shared.OrderDtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Service.MappingProfiles;

public class OrderItemPictureUrlResolver(IConfiguration config) : IPictureUrlResolver
{
    public string Resolve(string sourceUrl)
    {
        var baseUrl = config["URLs:BaseUrl"];

        if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(sourceUrl))
            return string.Empty;

        if (sourceUrl.StartsWith("http"))
            return sourceUrl;

        return $"{baseUrl.TrimEnd('/')}/{sourceUrl.TrimStart('/')}";
    }
}
