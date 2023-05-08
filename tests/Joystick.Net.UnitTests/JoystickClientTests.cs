using System;
using Joystick.Net.Client;
using Xunit;

namespace Joystick.Net.UnitTests
{
    public class JoystickClientTests
    {
        [Fact]
        public void DummyTest()
        {
            var config = new JoystickClientConfiguration();

            Assert.Throws<ArgumentNullException>(() => new JoystickClient(config));
        }
    }
}