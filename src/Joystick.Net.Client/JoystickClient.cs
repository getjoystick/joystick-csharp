using System;

namespace Joystick.Net.Client
{
    public class JoystickClient : IJoystickClient
    {
        private JoystickClientConfiguration configuration;

        public JoystickClient(JoystickClientConfiguration configuration)
        {
            if (configuration?.ApiKey == null)
            {
                throw new ArgumentNullException(configuration?.ApiKey);
            }

            this.configuration = configuration;
        }
    }
}