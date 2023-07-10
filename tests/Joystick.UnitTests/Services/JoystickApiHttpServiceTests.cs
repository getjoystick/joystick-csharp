using System.Net;
using System.Threading.Tasks;
using Joystick.Client.Exceptions;
using Joystick.Client.Services.Http;
using Joystick.UnitTests.Helpers;
using Xunit;

namespace Joystick.UnitTests.Services
{
    public class JoystickApiHttpServiceTests
    {
        [Fact]
        public async Task GetJsonContentsAsync_ShouldReturn_SerializedHttpResponseBody()
        {
            var expectedJsonContent = DataHelper.GetSerializedContent();
            var httpClient = Helper.GetMockedHttpClient(HttpStatusCode.OK, expectedJsonContent);
            var httpService = new JoystickApiHttpService(httpClient);

            var actualJsonContent = await httpService.GetJsonContentsAsync(new[] { "someId" }, Helper.CreateGetContentSettings());

            Assert.Equal(expectedJsonContent, actualJsonContent);
        }

        [Theory]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.GatewayTimeout)]
        public async Task GetJsonContentsAsync_ShouldReturn_JoystickApiServerException(HttpStatusCode statusCode)
        {
            var httpClient = Helper.GetMockedHttpClient(statusCode, string.Empty);
            var httpService = new JoystickApiHttpService(httpClient);

            await Assert.ThrowsAsync<JoystickApiServerException>(() => httpService.GetJsonContentsAsync(new[] { "someId" }, Helper.CreateGetContentSettings()));
        }

        [Theory]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.Unauthorized)]
        public async Task GetJsonContentsAsync_ShouldReturn_JoystickApiBadRequestException(HttpStatusCode statusCode)
        {
            var httpClient = Helper.GetMockedHttpClient(statusCode, string.Empty);
            var httpService = new JoystickApiHttpService(httpClient);

            await Assert.ThrowsAsync<JoystickApiBadRequestException>(() => httpService.GetJsonContentsAsync(new[] { "someId" }, Helper.CreateGetContentSettings()));
        }

        [Theory]
        [InlineData(HttpStatusCode.Continue)]
        [InlineData(HttpStatusCode.Ambiguous)]
        public async Task GetJsonContentsAsync_ShouldReturn_JoystickApiUnknownException(HttpStatusCode statusCode)
        {
            var httpClient = Helper.GetMockedHttpClient(statusCode, string.Empty);
            var httpService = new JoystickApiHttpService(httpClient);

            await Assert.ThrowsAsync<JoystickApiUnknownException>(() => httpService.GetJsonContentsAsync(new[] { "someId" }, Helper.CreateGetContentSettings()));
        }

        [Theory]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.GatewayTimeout)]
        public async Task UpsertJsonContentAsync_ShouldReturn_JoystickApiServerException(HttpStatusCode statusCode)
        {
            var httpClient = Helper.GetMockedHttpClient(statusCode, string.Empty);
            var httpService = new JoystickApiHttpService(httpClient);


            await Assert.ThrowsAsync<JoystickApiServerException>(() => httpService.UpsertJsonContentAsync( "someId", DataHelper.GetUpsertContentRequestBody(), DataHelper.GetJoystickClientConfig() ));
        }

        [Theory]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.Unauthorized)]
        public async Task UpsertJsonContentAsync_ShouldReturn_JoystickApiBadRequestException(HttpStatusCode statusCode)
        {
            var httpClient = Helper.GetMockedHttpClient(statusCode, string.Empty);
            var httpService = new JoystickApiHttpService(httpClient);

            await Assert.ThrowsAsync<JoystickApiBadRequestException>(() => httpService.UpsertJsonContentAsync("someId", DataHelper.GetUpsertContentRequestBody(), DataHelper.GetJoystickClientConfig()));
        }

        [Theory]
        [InlineData(HttpStatusCode.Continue)]
        [InlineData(HttpStatusCode.Ambiguous)]
        public async Task UpsertJsonContentAsync_ShouldReturn_JoystickApiUnknownException(HttpStatusCode statusCode)
        {
            var httpClient = Helper.GetMockedHttpClient(statusCode, string.Empty);
            var httpService = new JoystickApiHttpService(httpClient);

            await Assert.ThrowsAsync<JoystickApiUnknownException>(() => httpService.UpsertJsonContentAsync("someId", DataHelper.GetUpsertContentRequestBody(), DataHelper.GetJoystickClientConfig()));
        }
    }
}
