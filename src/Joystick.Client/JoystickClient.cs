using System;
using Joystick.Client.Models;

namespace Joystick.Client
{
    public class JoystickClient : IJoystickClient
    {
        private JoystickClientConfiguration configuration;

        public JoystickClient(JoystickClientConfiguration configuration)
        {
            if (configuration?.ApiKey == null)
            {
                throw new ArgumentNullException(nameof(configuration.ApiKey));
            }

            this.configuration = configuration;
        }
    }
}