using System.Threading.Tasks;
using Joystick.Client.Models;
using Newtonsoft.Json.Linq;

namespace Joystick.Client
{
    public interface IJoystickClient
    {
        Task<JoystickFullContentResponse<TData>>
            GetFullContentAsync<TData>(string contentId, JoystickContentOptions options = null);

        Task<JoystickFullContentResponse<JObject>>
            GetFullContentAsync(string contentId, JoystickContentOptions options = null);

        Task<TData> GetContentAsync<TData>(string contentId, JoystickContentOptions options = null);

        Task<JObject> GetContentAsync(string contentId, JoystickContentOptions options = null);
    }
}
