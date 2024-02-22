using Microsoft.Extensions.Caching.Memory;
using System;

namespace Rebels.ShortLink.Api.Services
{
    public class ShortLinkService : IShortLinkService
    {
        private readonly IEncodeShortLinkService _encodeShortLink;
        private readonly ICounterService _counterService;
        private readonly HttpContext _httpContext;
        private readonly IMemoryCache _memoryCache;

        public ShortLinkService(IEncodeShortLinkService encodeShortLink, ICounterService counterService, IHttpContextAccessor httpContextAccessor, IMemoryCache memoryCache)
        {
            _encodeShortLink = encodeShortLink;
            _counterService = counterService;
            _httpContext = httpContextAccessor.HttpContext!;
            _memoryCache = memoryCache;
        }

        public (string, string) GenerateShortLink(string url)
        {
            var id = _memoryCache.Get(url)?.ToString();
            var shortLinkPrefix = $"{_httpContext.Request.Scheme}://{_httpContext.Request.Host}";
            if (!string.IsNullOrEmpty(id))
                return (id, $"{shortLinkPrefix}/{id}");
            var newId = _encodeShortLink.Encode(_counterService.GetCounter());
            var newShortLink = $"{shortLinkPrefix}/{newId}";
            _memoryCache.Set(newId, url, TimeSpan.FromHours(1));
            _memoryCache.Set(url, newId, TimeSpan.FromHours(1));
            _counterService.UpdateCounter();
            return (newId, newShortLink);
        }

        public string? GetOriginalUrl(string id)
        {
            var originalUrl = _memoryCache.Get(id);
            return originalUrl?.ToString();
        }
    }
}
