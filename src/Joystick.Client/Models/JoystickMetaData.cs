namespace Joystick.Client.Models
{
    public class JoystickMetaData
    {
        public uint Uid { get; set; }

        public int Mod { get; set; }

        public object[] Variants { get; set; }

        public object[] Seg { get; set; }
    }
}
