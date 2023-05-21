using System;
using System.Net;

namespace Joystick.Client.Exceptions
{
    public class JoystickApiBadRequestException : JoystickApiHttpException
    {
        public JoystickApiBadRequestException(string message)
            : base(message)
        {
        }

        public JoystickApiBadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public JoystickApiBadRequestException(HttpStatusCode statusCode)
            : base(statusCode)
        {
        }
    }
}
