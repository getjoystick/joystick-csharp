using System;
using System.Net;

namespace Joystick.Client.Exceptions
{
    public class JoystickApiUnknownException : JoystickApiHttpException
    {
        public JoystickApiUnknownException(string message)
            : base(message)
        {
        }

        public JoystickApiUnknownException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public JoystickApiUnknownException(HttpStatusCode statusCode, Exception innerException)
            : base(statusCode, innerException)
        {
        }
    }
}
