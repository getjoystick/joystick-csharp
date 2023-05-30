using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joystick.Client.Models
{
    public class JoystickFullContentResponse<T>
    {
        public JoystickMetaData Meta { get; set; }

        public string Hash { get; set; }

        public T Data { get; set; }
    }
}
