using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JoystickClient.Client;
using NUnit.Framework;

namespace JoystickClient.Tests.ApiTest
{
    [TestFixture]
    internal class InfoApiTest
    {
        private JoystickApiClient _joystickApiClient;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _joystickApiClient = new(Guid.NewGuid().ToString());
        }

        [Test]
        public async Task InfoApi_CancelationTokenTests()
        {
            const string excMessage = "task was canceled";
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            var joystickApiException = Assert.ThrowsAsync<JoystickApiException>(async () => await _joystickApiClient.Info.GetApiVersionAsync(cancellationTokenSource.Token));
            StringAssert.Contains(excMessage, joystickApiException.Message.ToLowerInvariant());
        }

        [Test]
        public async Task InfoApi_EnsuresAuthenticationNotRequiredTest()
        {
            try
            {
                await _joystickApiClient.Info.GetApiVersionAsync();
            }
            catch (JoystickApiException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                Assert.Fail("The GetApiVersion method does not require authentication");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [Test]
        public async Task InfoApi_GetApiVersionTest()
        {
            var apiVersion = await _joystickApiClient.Info.GetApiVersionAsync();
            Assert.NotNull(apiVersion);
            Assert.That(apiVersion, Has.Length.GreaterThan(0));
        }
    }
}
