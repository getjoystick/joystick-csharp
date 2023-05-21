using System;

namespace Joystick.Client.Exceptions
{
    public class JoystickException : Exception
    {
        public JoystickException(string message)
            : base(message)
        {
        }


        public JoystickException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
