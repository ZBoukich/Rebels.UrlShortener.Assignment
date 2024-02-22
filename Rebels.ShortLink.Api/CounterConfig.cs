using System.ComponentModel.DataAnnotations;

namespace Rebels.ShortLink.Api
{
    public class CounterConfig
    {
        [Required]
        public string Key { get; init; } = null!;
        [Required]
        public long? StartingUid { get; init; }
        [Required]
        public long? EndingUid { get; init; }
    }
}
