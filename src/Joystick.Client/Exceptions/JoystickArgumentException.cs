using System;

namespace Joystick.Client.Exceptions
{
    public class JoystickArgumentException : JoystickException
    {
        internal JoystickArgumentException(string message)
            : base(message)
        {
        }

        internal JoystickArgumentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
