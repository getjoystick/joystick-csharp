using System;

namespace Joystick.Client.Exceptions
{
    public class JoystickArgumentException : JoystickException
    {
        public JoystickArgumentException(string message)
            : base(message)
        {
        }

        public JoystickArgumentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
