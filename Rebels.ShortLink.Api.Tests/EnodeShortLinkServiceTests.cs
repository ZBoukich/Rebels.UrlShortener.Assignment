using Rebels.ShortLink.Api.Services;

namespace Rebels.ShortLink.Api.Tests
{
    public class EnodeShortLinkServiceTests
    {
        [Fact]
        public void EncodeShortLink_Positive()
        {
            //Arrange
            var encodeShortLinkService = new EncodeShortLinkService();
            const long id = 250000L;
            const string expectedValue = "132g";

            //Act
            var hashStr = encodeShortLinkService.Encode(id);

            //Assert
            Assert.True(hashStr.Equals(expectedValue));
        }

        [Fact]
        public void EncodeShortLink_WhenInputIsZero_Negativ()
        {
            //Arrange
            var encodeShortLinkService = new EncodeShortLinkService();
            const long id = 0L;

            //Act
            var hashStr = encodeShortLinkService.Encode(id);

            //Assert
            Assert.Empty(hashStr);
        }

        [Fact]
        public void EncodeShortLink_WhenInputIsNegativNumber_Negativ()
        {
            //Arrange
            var encodeShortLinkService = new EncodeShortLinkService();
            const long id = -10L;

            //Act
            var hashStr = encodeShortLinkService.Encode(id);

            //Assert
            Assert.Empty(hashStr);
        }
    }
}