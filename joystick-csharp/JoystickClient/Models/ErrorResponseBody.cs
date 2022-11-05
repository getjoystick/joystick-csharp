using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoystickClient.Models
{
    public sealed class ErrorResponseBody
    {
        public string Data { get; set; }
        public int Status { get; set; }
        public string message { get; set; }
        public string[] Details { get; set; }
    }
}
