using System.Threading.Tasks;
using Joystick.Client.Models.Api;

namespace Joystick.Client.Services.Http
{
    public interface IJoystickApiHttpService
    {
        Task<string> GetContentJsonAsync(string contentId, GetContentSettings config);
    }
}
