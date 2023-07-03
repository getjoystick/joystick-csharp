using System;

namespace Joystick.Client.Exceptions
{
    public class JoystickConfigurationException : JoystickException
    {
        public JoystickConfigurationException(string message)
            : base(message)
        {
        }

        public JoystickConfigurationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
