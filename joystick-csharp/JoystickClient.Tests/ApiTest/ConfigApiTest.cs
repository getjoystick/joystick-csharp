using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JoystickClient.Api;
using JoystickClient.Client;

namespace JoystickClient.Tests.ApiTest
{
    [TestFixture]
    internal class ConfigApiTest
    {
        private JoystickApiClient _joystickApiClient;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _joystickApiClient = new(Guid.NewGuid().ToString());
        }

        [Test]
        public async Task ConfigApi_ParametersValidationTest()
        {
            Assert.Multiple(() =>
            {
                Assert.ThrowsAsync<ArgumentNullException>(async () => await _joystickApiClient.Config.GetConfigAsync(null));
                Assert.ThrowsAsync<ArgumentNullException>(async () => await _joystickApiClient.Config.GetDynamicConfig(null, null, null));
                Assert.ThrowsAsync<ArgumentNullException>(async () => await _joystickApiClient.Config.GetDynamicConfig("cnf", null, null));
                Assert.ThrowsAsync<ArgumentNullException>(async () => await _joystickApiClient.Config.GetDynamicConfigHashAsync(null, null, null));
                Assert.ThrowsAsync<ArgumentNullException>(async () => await _joystickApiClient.Config.GetVeryDynamicConfigAsync(null, null, null));
                Assert.ThrowsAsync<ArgumentNullException>(async () => await _joystickApiClient.Config.GetVeryDynamicConfigHashAsync(null, null, null));
            });
        }

        [Test]
        public async Task ConfigApi_CancelationTokenTests()
        {
            const string excMessage = "task was canceled";
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            Assert.Multiple(() =>
            {
                var joystickApiException = Assert.ThrowsAsync<JoystickApiException>(async () => await _joystickApiClient.Config.GetConfigAsync("cnf", cancellationTokenSource.Token));
                StringAssert.Contains(excMessage, joystickApiException.Message.ToLowerInvariant());

                joystickApiException = Assert.ThrowsAsync<JoystickApiException>(async () => await _joystickApiClient.Config.GetDynamicConfig("cnf", "userId", null, cancellationTokenSource.Token));
                StringAssert.Contains(excMessage, joystickApiException.Message.ToLowerInvariant());

                joystickApiException = Assert.ThrowsAsync<JoystickApiException>(async () => await _joystickApiClient.Config.GetDynamicConfigHashAsync("cnf", "userId", null, cancellationTokenSource.Token));
                StringAssert.Contains(excMessage, joystickApiException.Message.ToLowerInvariant());

                joystickApiException = Assert.ThrowsAsync<JoystickApiException>(async () => await _joystickApiClient.Config.GetVeryDynamicConfigAsync("cnf", "userId", null,
                    token: cancellationTokenSource.Token));
                StringAssert.Contains(excMessage, joystickApiException.Message.ToLowerInvariant());

                joystickApiException = Assert.ThrowsAsync<JoystickApiException>(async () => await _joystickApiClient.Config.GetVeryDynamicConfigHashAsync("cnf", "userId", null, token: cancellationTokenSource.Token));
                StringAssert.Contains(excMessage, joystickApiException.Message.ToLowerInvariant());
            });
        }

        [Test]
        public async Task ConfigApi_EnsuresAuthenticationFailedTest()
        {
            var joystickApiException = Assert.ThrowsAsync<JoystickApiException>(async () => await _joystickApiClient.Config.GetConfigAsync("cnf"));
            Assert.That(joystickApiException.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}
