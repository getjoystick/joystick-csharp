using System;

namespace Joystick.Client.Exceptions
{
    public class JoystickConfigurationException : JoystickArgumentException
    {
        internal JoystickConfigurationException(string message)
            : base(message)
        {
        }

        internal JoystickConfigurationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
