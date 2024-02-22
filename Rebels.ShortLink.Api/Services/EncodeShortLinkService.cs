namespace Rebels.ShortLink.Api.Services
{
    public class EncodeShortLinkService : IEncodeShortLinkService
    {
        public string Encode(long id)
        {
            var s = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var hashStr = "";
            while (id > 0)
            {
                hashStr = s[(int)(id % s.Length)] + hashStr;
                id /= s.Length;
            }
            return hashStr;
        }
    }
}
