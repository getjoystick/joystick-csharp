using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Joystick.Client;
using Joystick.Client.Exceptions;
using Joystick.Client.Models;
using Joystick.UnitTests.Helpers;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.Protected;
using Xunit;

namespace Joystick.UnitTests
{
    public class JoystickClientTests
    {
        [Fact]
        public void JoystickClientConstructor_ShouldThrowException_WhenConfigIsNull()
        {
            Assert.Throws<JoystickArgumentException>(() => new JoystickClient(null));
        }

        [Fact]
        public void JoystickClientConstructor_ShouldThrowException_WhenConfigInvalid()
        {
            var config = new JoystickClientConfig();

            Assert.Throws<JoystickConfigurationException>(() => new JoystickClient(config));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task PublishContentUpdateAsync_ShouldThrowException_WhenConfigInvalid(string contentId)
        {
            var client = new JoystickClient(DataHelper.GetJoystickClientConfig());

            var exception = await Assert.ThrowsAsync<JoystickArgumentException>(() => client.PublishContentUpdateAsync(contentId, new JoystickPublishContentPayload()));
            Assert.Contains("contentId", exception.Message);
        }

        [Fact]
        public async Task PublishContentUpdateAsync_ShouldThrowException_WhenPayloadIsNull()
        {
            var client = new JoystickClient(DataHelper.GetJoystickClientConfig());

            var exception = await Assert.ThrowsAsync<JoystickArgumentException>(() => client.PublishContentUpdateAsync("contentId", null));
            Assert.Contains("payload", exception.Message);
        }
    }
}