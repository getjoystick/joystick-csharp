using System;
using Joystick.Client;
using Joystick.Client.Exceptions;
using Joystick.Client.Models;
using Xunit;

namespace Joystick.UnitTests
{
    public class JoystickClientTests
    {
        [Fact]
        public void JoystickClientConstructor_ShouldThrowException_WhenConfigIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new JoystickClient(null));
        }

        [Fact]
        public void JoystickClientConstructor_ShouldThrowException_WhenConfigInvalid()
        {
            var config = new JoystickClientConfig();

            Assert.Throws<JoystickException>(() => new JoystickClient(config));
        }
    }
}