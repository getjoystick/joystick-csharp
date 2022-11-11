using System;
using System.Collections.Generic;
using System.Text;
using JoystickClient.Clients;

namespace JoystickClient.Interfaces
{
    public interface IJoystickApiClient
    {
        IConfigApi Config { get; }
        IEnvironmentApi Environment { get; }
        IInfoApi Info { get; }
    }
}
