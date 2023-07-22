using System;

namespace Joystick.Client.Exceptions
{
    public class JoystickConfigurationException : JoystickArgumentException
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
