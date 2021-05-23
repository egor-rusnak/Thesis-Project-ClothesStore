using Microsoft.AspNetCore.Http;

namespace ClothesStore.WebUI.Extensions
{
    public static class UrlExtensions
    {
        public static string PathAnQuery(this HttpRequest request) =>
            request.QueryString.HasValue ?
            $"{request.Path}{request.QueryString}"
            : request.Path.ToString();
    }
}
