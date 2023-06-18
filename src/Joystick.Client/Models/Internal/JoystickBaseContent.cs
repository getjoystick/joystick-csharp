using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Joystick.UnitTests")]

namespace Joystick.Client.Models.Internal
{
    internal class JoystickBaseContent<T>
    {
        public T Data { get; set; }
    }
}
