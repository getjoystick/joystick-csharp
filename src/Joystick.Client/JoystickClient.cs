using System;
using System.Net.Http;
using System.Threading.Tasks;
using Joystick.Client.Models;
using Joystick.Client.Services;
using Joystick.Client.Utils;
using Newtonsoft.Json.Linq;

namespace Joystick.Client
{
    public class JoystickClient : IJoystickClient
    {
        private readonly JoystickClientConfig config;
        private readonly IJoystickApiHttpService httpService;
        private readonly IJsonOutputSerializer outputSerializer;

        public JoystickClient(
            JoystickClientConfig config,
            IJoystickApiHttpService httpService = null,
            IJsonOutputSerializer outputSerializer = null)
        {
            if (config?.ApiKey == null)
            {
                throw new ArgumentNullException(nameof(config.ApiKey));
            }

            this.config = config.Clone();
            this.httpService = httpService ?? new JoystickApiHttpService(new HttpClient());
            this.outputSerializer = outputSerializer ?? new JoystickDefaultJsonOutputSerializer();
        }

        public async Task<JoystickFullContentResponse<TData>> GetFullContentAsync<TData>(string contentId, JoystickContentOptions options = null)
        {
            var responseBody = await this.GetSerializedFullContentAsync(contentId, options);
            return this.outputSerializer.Deserialize<JoystickFullContentResponse<TData>>(responseBody);
        }

        public Task<JoystickFullContentResponse<JObject>> GetFullContentAsync(string contentId, JoystickContentOptions options = null)
        {
            return this.GetFullContentAsync<JObject>(contentId, options);
        }

        public async Task<TData> GetContentAsync<TData>(string contentId, JoystickContentOptions options = null)
        {
            var fullContent = await this.GetFullContentAsync<TData>(contentId, options);
            return fullContent.Data;
        }

        public async Task<JObject> GetContentAsync(string contentId, JoystickContentOptions options = null)
        {
            var fullContent = await this.GetFullContentAsync(contentId, options);
            return fullContent.Data;
        }

        private Task<string> GetSerializedFullContentAsync(string contentId, JoystickContentOptions options = null)
        {
            if (string.IsNullOrWhiteSpace(contentId))
            {
                throw new ArgumentException($"{nameof(contentId)} can't be empty");
            }

            return this.httpService.GetContentJsonAsync(contentId, this.config);
        }
    }
}