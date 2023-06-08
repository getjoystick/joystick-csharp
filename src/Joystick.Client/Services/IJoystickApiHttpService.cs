using System.Threading.Tasks;
using Joystick.Client.Models;

namespace Joystick.Client.Services
{
    public interface IJoystickApiHttpService
    {
        Task<string> GetContentJsonAsync(string contentId, JoystickClientConfig config);
    }
}
