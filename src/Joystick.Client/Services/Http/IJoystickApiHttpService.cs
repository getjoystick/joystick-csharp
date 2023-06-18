using System.Collections.Generic;
using System.Threading.Tasks;
using Joystick.Client.Models.Api;

namespace Joystick.Client.Services.Http
{
    public interface IJoystickApiHttpService
    {
        Task<string> GetJsonContentsAsync(IEnumerable<string> contentIds, GetContentSettings settings);
    }
}
