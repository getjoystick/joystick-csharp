namespace Joystick.Client.Models
{
    public class JoystickFullContent<T>
    {
        public JoystickMetaData Meta { get; set; }

        public string Hash { get; set; }

        public T Data { get; set; }
    }
}
