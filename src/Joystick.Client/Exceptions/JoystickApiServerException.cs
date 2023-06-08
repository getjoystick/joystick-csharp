using System;
using System.Net;

namespace Joystick.Client.Exceptions
{
    public class JoystickApiServerException : JoystickApiHttpException
    {
        public JoystickApiServerException(string message)
            : base(message)
        {
        }

        public JoystickApiServerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public JoystickApiServerException(HttpStatusCode statusCode, Exception innerException)
            : base(statusCode, innerException)
        {
        }
    }
}
