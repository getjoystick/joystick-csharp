using System;
using System.Net;

namespace Joystick.Client.Exceptions
{
    public class JoystickApiHttpException : JoystickException
    {
        public JoystickApiHttpException(string message)
            : base(message)
        {
        }

        public JoystickApiHttpException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public JoystickApiHttpException(HttpStatusCode statusCode, Exception innerException)
            : base($"Request to Joystick failed with status code {statusCode}", innerException)
        {
        }
    }
}
