using ECommerce.ServiceAbstraction;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Service.MappingProfiles;

public class PictureUrlResolver(IConfiguration config) : IPictureUrlResolver
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