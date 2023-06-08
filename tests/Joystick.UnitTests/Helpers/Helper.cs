using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Joystick.Client.Models;
using Joystick.Client.Models.Api;
using Moq;
using Moq.Protected;

namespace Joystick.UnitTests.Helpers
{
    public static class Helper
    {
        public static HttpClient GetMockedHttpClient(HttpStatusCode statusCode, string content)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(content),
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
            return new HttpClient(handlerMock.Object);
        }

        public static GetContentSettings CreateGetContentSettings()
        {
            return new GetContentSettings(new JoystickClientConfig(), new JoystickContentOptions(), false);
        }
    }
}
