namespace Rebels.ShortLink.Api.Services
{
    public interface ICounterService
    {
        long GetCounter();
        void UpdateCounter();
    }
}
