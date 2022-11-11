using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JoystickClient.Client;

namespace JoystickClient.Tests.ApiTest
{
    [TestFixture]
    internal class EnvironmentApiTest
    {
        private JoystickApiClient _joystickApiClient;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _joystickApiClient = new(Guid.NewGuid().ToString());
        }       

        [Test]
        public async Task EnvironmentApi_CancelationTokenTests()
        {
            const string excMessage = "task was canceled";
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            var joystickApiException = Assert.ThrowsAsync<JoystickApiException>(async () => await _joystickApiClient.Environment.GetEnvironmentCatalogAsync(cancellationTokenSource.Token));
            StringAssert.Contains(excMessage, joystickApiException.Message.ToLowerInvariant());
        }

        [Test]
        public async Task EnvironmentApi_EnsuresAuthenticationFailedTest()
        {
            var joystickApiException = Assert.ThrowsAsync<JoystickApiException>(async () => await _joystickApiClient.Environment.GetEnvironmentCatalogAsync());
            Assert.That(joystickApiException.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}
