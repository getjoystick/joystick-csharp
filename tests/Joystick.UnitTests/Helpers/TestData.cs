using System.Collections.Generic;

namespace Joystick.UnitTests.Helpers
{
    public class DesignConfigs
    {
        public DesignTheme Theme { get; set; }

        public string Greeting { get; set; }

        public float[] Scales { get; set; }
    }

    public enum DesignTheme
    {
        None = 0,
        Dark = 1,
        Light = 2
    }
}
