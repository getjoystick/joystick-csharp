using System;
using System.Net;

namespace Joystick.Client.Exceptions
{
    public class JoystickApiServerException : JoystickApiHttpException
    {
        internal JoystickApiServerException(string message)
            : base(message)
        {
        }

        internal JoystickApiServerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        internal JoystickApiServerException(HttpStatusCode statusCode, Exception innerException)
            : base(statusCode, innerException)
        {
        }
    }
}
