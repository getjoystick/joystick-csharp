using System.Collections.Generic;

namespace Joystick.UnitTests.Helpers
{
    public enum DesignTheme
    {
        /// <summary>
        /// Design Theme in note chosen
        /// </summary>
        None = 0,

        /// <summary>
        /// Design Theme is Dark
        /// </summary>
        Dark = 1,

        /// <summary>
        /// Design Theme is Light
        /// </summary>
        Light = 2,
    }

    public class DesignConfigs
    {
        public DesignTheme Theme { get; set; }

        public string? Greeting { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1011:Closing square brackets should be spaced correctly", Justification ="Conflict of rules")]
        public float[]? Scales { get; set; }
    }
}
