using System;
using System.Net;

namespace Joystick.Client.Exceptions
{
    public class JoystickApiHttpException : JoystickException
    {
        internal JoystickApiHttpException(string message)
            : base(message)
        {
        }

        internal JoystickApiHttpException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        internal JoystickApiHttpException(HttpStatusCode statusCode, Exception innerException)
            : base($"Request to Joystick failed with status code {statusCode}", innerException)
        {
        }
    }
}
