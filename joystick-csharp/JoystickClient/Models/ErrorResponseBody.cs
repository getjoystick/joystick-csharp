using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoystickClient.Models;

public sealed record ErrorResponseBody
{
    public string Data { get; init; }
    public int Status { get; init; }
    public string message { get; init; }
    public string[] Details { get; init; }
}    
