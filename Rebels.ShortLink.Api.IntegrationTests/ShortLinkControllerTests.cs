using Microsoft.AspNetCore.Mvc.Testing;
using Rebels.ShortLink.Api.Controllers;
using System.Net;
using System.Net.Http.Json;

namespace Rebels.ShortLink.Api.IntegrationTests
{
    public class ShortLinkControllerTests
    {
        private readonly HttpClient _httpClient;

        public ShortLinkControllerTests()
        {
            var webApplicationFactory = new WebApplicationFactory<Program>();
            _httpClient = webApplicationFactory.CreateDefaultClient();
        }

        [Fact]
        public async Task Encode_WhenInputIsValid_ReturnsShortenedUrl()
        {
            //Arrange
            var response = await _httpClient.PostAsJsonAsync("encode", new { Url = "https://www.nu.nl"});

            //Act
            var result = await response.Content.ReadFromJsonAsync<EncodeResponse>();

            //Assert
            Assert.Equivalent(response.StatusCode, HttpStatusCode.OK);
            Assert.IsType<EncodeResponse>(result);
            Assert.NotNull(result.Id);
            Assert.NotNull(result.ShortLink);
        }

        [Fact]
        public async Task Encode_WhenInputIsInValid_ReturnsBadRequest_Negative()
        {
            //Arrange
            var response = await _httpClient.PostAsJsonAsync("encode", new { Url = "/wQhtps" });

            //Act
            var result = await response.Content.ReadFromJsonAsync<EncodeResponse>();

            //Assert
            Assert.Equivalent(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.Null(result.Id);
            Assert.Null(result.ShortLink);
        }


        [Fact]
        public void Decode_WhenInputIsValid_ReturnsOriginalUrl_Positive()
        {

            //Arrange


            //Act


            //Assert

        }

        [Fact]
        public void Decode_WhenInputIsEmpty_ReturnsBadRequest_Negative()
        {

            //Arrange


            //Act


            //Assert

        }

        [Fact]
        public void Decode_When_OriginalUrlIsNotFound_ReturnsNotFound_Negative()
        {

            //Arrange


            //Act


            //Assert

        }
    }
}