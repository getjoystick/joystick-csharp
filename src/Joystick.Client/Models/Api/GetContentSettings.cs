using System;

namespace Joystick.Client.Models.Api
{
    internal class GetContentSettings
    {
        public GetContentSettings(JoystickContentOptions contentOptions, bool isContentSerialized)
        {
            this.IsContentSerialized = isContentSerialized;
            this.ContentOptions = contentOptions;
        }

        public GetContentSettings(JoystickContentOptions contentOptions, Type dataType)
        {
            this.IsContentSerialized = dataType == typeof(string);
            this.ContentOptions = contentOptions;
        }

        public bool IsContentSerialized { get; set; }

        public JoystickContentOptions ContentOptions { get; set; }
    }
}
