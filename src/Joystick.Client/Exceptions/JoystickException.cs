using System;

namespace Joystick.Client.Exceptions
{
    public class JoystickException : Exception
    {
        internal JoystickException(string message)
            : base(message)
        {
        }

        internal JoystickException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
