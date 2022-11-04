using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoystickClient.Client;

namespace JoystickClient.Interfaces;

internal interface IJoystickApiHttpClient
{
    Task<JoystickApiResponse> SendRequestAsync(JoystickApiRequest request, CancellationToken cancellationToken);
}
