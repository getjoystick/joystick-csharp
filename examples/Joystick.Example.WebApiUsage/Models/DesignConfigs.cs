namespace Joystick.Example.WebApiUsage.Models
{
    public class DesignConfigs
    {
        public DesignTheme Theme { get; set; }

        public string? Greeting { get; set; }

        public int Scale { get; set; }
    }

    public enum DesignTheme
    {
        None = 0,
        Dark = 1,
        Light = 2
    }
}
