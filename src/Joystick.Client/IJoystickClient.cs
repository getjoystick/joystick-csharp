using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Joystick.Client.Models;
using Newtonsoft.Json.Linq;

namespace Joystick.Client
{
    public interface IJoystickClient
    {
        Task<JoystickFullContent<TData>>
            GetFullContentAsync<TData>(string contentId, JoystickContentOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<JoystickFullContent<JObject>>
            GetFullContentAsync(string contentId, JoystickContentOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<TData> GetContentAsync<TData>(string contentId, JoystickContentOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<JObject> GetContentAsync(string contentId, JoystickContentOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<Dictionary<string, JoystickFullContent<JObject>>>
            GetFullContentsAsync(IEnumerable<string> contentIds, JoystickContentOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<Dictionary<string, JoystickFullContent<TData>>>
           GetFullContentsAsync<TData>(IEnumerable<string> contentIds, JoystickContentOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<Dictionary<string, JObject>> GetContentsAsync(IEnumerable<string> contentIds, JoystickContentOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<Dictionary<string, TData>> GetContentsAsync<TData>(IEnumerable<string> contentIds, JoystickContentOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        Task PublishContentUpdateAsync(string contentId, JoystickPublishContentPayload payload, CancellationToken cancellationToken = default(CancellationToken));

        void ClearCache();
    }
}
