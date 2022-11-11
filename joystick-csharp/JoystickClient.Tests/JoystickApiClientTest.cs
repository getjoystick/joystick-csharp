using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using JoystickClient.Client;
using JoystickClient.Clients;
using JoystickClient.Interfaces;
using JoystickClient.Models.ApiResponse;
using Newtonsoft.Json.Linq;

namespace JoystickClient.Tests
{
    [TestFixture]
    internal class JoystickApiClientTest
    {
        private JoystickApiClient _joystickApiClient;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _joystickApiClient = new(Guid.NewGuid().ToString());
        }

        [Test]
        public async Task ApiPropertiesTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_joystickApiClient.Config, Is.InstanceOf<IConfigApi>(), "Config is IConfigApi");
                Assert.That(_joystickApiClient.Environment, Is.InstanceOf<IEnvironmentApi>(), "Environment is IEnvironmentApi");
                Assert.That(_joystickApiClient.Info, Is.InstanceOf<IInfoApi>(), "Info is IInfoApi");
            });
        }

        [Test]
        public void EnsuresArgumentsNotNullNorEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => new JoystickApiClient(null));
            Assert.Throws<ArgumentNullException>(() => new JoystickApiClient(" "));
        }

        [Test]
        public async Task CustomHttpClientUsageTest()
        {
            using var httpClient = new HttpClient();

            httpClient.Timeout = TimeSpan.FromMilliseconds(100);

            var joystickApiClient = new JoystickApiClient(Guid.NewGuid().ToString(), httpClient);

            Assert.That(async () => await joystickApiClient.Info.GetApiVersionAsync(), Throws.TypeOf<JoystickApiException>()
                .With.Message.Contains("Timeout"));
        }
    }
}
