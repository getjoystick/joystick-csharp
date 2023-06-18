using System;

namespace Joystick.Client.Exceptions
{
    public class MultipleContentsApiException : JoystickException
    {
        public MultipleContentsApiException(string message)
            : base(message)
        {
        }

        public MultipleContentsApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
