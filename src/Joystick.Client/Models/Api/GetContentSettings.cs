using System;

namespace Joystick.Client.Models.Api
{
    internal class GetContentSettings
    {
        public GetContentSettings(JoystickContentOptions contentOptions, bool isContentSerialized)
        {
            this.IsContentSerialized = isContentSerialized;
            this.Refresh = contentOptions?.Refresh;
        }

        public GetContentSettings(JoystickContentOptions contentOptions, Type dataType)
        {
            this.IsContentSerialized = dataType == typeof(string);
            this.Refresh = contentOptions?.Refresh;
        }

        public bool IsContentSerialized { get; set; }

        public bool? Refresh { get; set; }
    }
}
