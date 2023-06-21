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
        public async Task GetContentJsonAsync_ShouldReturn_SerializedHttpResponseBody()
        {
            var expectedJsonContent = DataHelper.GetSerializedContent();
            var httpClient = Helper.GetMockedHttpClient(HttpStatusCode.OK, expectedJsonContent);
            var httpService = new JoystickApiHttpService(httpClient);

            var actualJsonContent = await httpService.GetContentJsonAsync("someId", Helper.CreateGetContentSettings());

            Assert.Equal(expectedJsonContent, actualJsonContent);
        }

        [Theory]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.GatewayTimeout)]
        public async Task GetContentJsonAsync_ShouldReturn_JoystickApiServerException(HttpStatusCode statusCode)
        {
            var httpClient = Helper.GetMockedHttpClient(statusCode, string.Empty);
            var httpService = new JoystickApiHttpService(httpClient);

            await Assert.ThrowsAsync<JoystickApiServerException>(() => httpService.GetContentJsonAsync("someId", Helper.CreateGetContentSettings()));
        }

        [Theory]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.Unauthorized)]
        public async Task GetContentJsonAsync_ShouldReturn_JoystickApiBadRequestException(HttpStatusCode statusCode)
        {
            var httpClient = Helper.GetMockedHttpClient(statusCode, string.Empty);
            var httpService = new JoystickApiHttpService(httpClient);

            await Assert.ThrowsAsync<JoystickApiBadRequestException>(() => httpService.GetContentJsonAsync("someId", Helper.CreateGetContentSettings()));
        }

        [Theory]
        [InlineData(HttpStatusCode.Continue)]
        [InlineData(HttpStatusCode.Ambiguous)]
        public async Task GetContentJsonAsync_ShouldReturn_JoystickApiUnknownException(HttpStatusCode statusCode)
        {
            var httpClient = Helper.GetMockedHttpClient(statusCode, string.Empty);
            var httpService = new JoystickApiHttpService(httpClient);

            await Assert.ThrowsAsync<JoystickApiUnknownException>(() => httpService.GetContentJsonAsync("someId", Helper.CreateGetContentSettings()));
        }
    }
}
