using Microsoft.AspNetCore.Mvc;
using Rebels.ShortLink.Api.Services;

namespace Rebels.ShortLink.Api.Controllers;

[ApiController]
[Route("/")]
public class ShortLinkController : ControllerBase
{
    private readonly IShortLinkService _shortLinkService;

    public ShortLinkController(IShortLinkService shortLinkService)
    {
        _shortLinkService = shortLinkService;
    }

    /// <summary>
    /// Encodes a URL to a shortened URL.
    /// </summary>
    /// <param name="request">The URL to be shortened.</param>
    /// <returns>A shortened URL.</returns>
    [HttpPost("encode")]
    [ProducesResponseType(typeof(EncodeResponse), StatusCodes.Status200OK)]
    public IActionResult Encode([FromBody] EncodeRequest request)
    {
        if (!Uri.TryCreate(request.Url, UriKind.Absolute, out var uriResult)
            && (uriResult?.Scheme != Uri.UriSchemeHttp || uriResult?.Scheme != Uri.UriSchemeHttps))
        {
            return BadRequest();
        }
        var (id, shortLink) = _shortLinkService.GenerateShortLink(request.Url);
        return Ok(new EncodeResponse(id, shortLink));
    }

    /// <summary>
    /// Decodes the id of a shortened URL to its original URL.
    /// </summary>
    /// <param name="id">The id of the shortened URL.</param>
    /// <returns>The original URL.</returns>
    [HttpGet("decode/{id}")]
    [ProducesResponseType(typeof(DecodeResponse), StatusCodes.Status200OK)]
    public IActionResult Decode(string id)
    {
        if(string.IsNullOrEmpty(id))return BadRequest();
        var originalUrl = _shortLinkService.GetOriginalUrl(id);
        if (string.IsNullOrEmpty(originalUrl))
            return NotFound();
        return Ok(new DecodeResponse(originalUrl));
    }

    /// <summary>
    /// Redirects id of a shortened URL to its original URL.
    /// </summary>
    /// <param name="id">The id of the shortened URL.</param>
    /// <returns>The original URL.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RedirectResponse), StatusCodes.Status302Found)]
    public IActionResult RedirectToOriginalUrl(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();
        var originalUrl = _shortLinkService.GetOriginalUrl(id);
        if (string.IsNullOrEmpty(originalUrl))
            return NotFound();
        return Redirect(originalUrl);
    }
}

// Do whatever you want with the items below, as long as the API's interface remains the same
public record struct EncodeRequest(string Url);
public record struct EncodeResponse(string Id, string ShortLink);
public record struct DecodeResponse(string OriginalUrl);
public record struct RedirectResponse(string OriginalUrl);