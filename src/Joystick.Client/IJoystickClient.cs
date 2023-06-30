using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Joystick.Client.Models;
using Newtonsoft.Json.Linq;

namespace Joystick.Client
{
    public interface IJoystickClient
    {
        Task<JoystickFullContent<TData>>
            GetFullContentAsync<TData>(string contentId, JoystickContentOptions options = null);

        Task<JoystickFullContent<JObject>>
            GetFullContentAsync(string contentId, JoystickContentOptions options = null);

        Task<TData> GetContentAsync<TData>(string contentId, JoystickContentOptions options = null);

        Task<JObject> GetContentAsync(string contentId, JoystickContentOptions options = null);

        Task<Dictionary<string, JoystickFullContent<JObject>>>
            GetFullContentsAsync(IEnumerable<string> contentIds, JoystickContentOptions options = null);

        Task<Dictionary<string, JoystickFullContent<TData>>>
           GetFullContentsAsync<TData>(IEnumerable<string> contentIds, JoystickContentOptions options = null);

        Task<Dictionary<string, JObject>> GetContentsAsync(IEnumerable<string> contentIds, JoystickContentOptions options = null);

        Task<Dictionary<string, TData>> GetContentsAsync<TData>(IEnumerable<string> contentIds, JoystickContentOptions options = null);
    }
}
