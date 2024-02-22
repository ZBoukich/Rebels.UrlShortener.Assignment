namespace Rebels.ShortLink.Api.Services
{
    public interface IShortLinkService
    {
        (string, string) GenerateShortLink(string url);
        string? GetOriginalUrl(string id);
    }
}