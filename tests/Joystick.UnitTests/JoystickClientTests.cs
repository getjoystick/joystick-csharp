using System;
using Joystick.Client;
using Joystick.Client.Models;
using Xunit;

namespace Joystick.UnitTests
{
    public class JoystickClientTests
    {
        [Fact]
        public void WhenApiKeyIsNull_JoystickClientConstructor_ShouldThrowException()
        {
            var config = new JoystickClientConfig();

            Assert.Throws<ArgumentNullException>(() => new JoystickClient(config));
        }
    }
}