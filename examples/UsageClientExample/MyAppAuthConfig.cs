using Joystick.Client.Models;

namespace UsageClientExample
{
    internal class MyAppAuthConfig: IJoystickContent
    {
        public string AuthUrl { get; set; }

        public string ClientSecret { get; set; }
    }
}
