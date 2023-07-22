using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Joystick.Client.Exceptions;
using Joystick.Client.Models;
using Joystick.Client.Models.Api;
using Joystick.Client.Models.Internal;
using Joystick.Client.Services.Http;
using Joystick.Client.Services.Serialization;
using Joystick.Client.Utils;
using Joystick.Client.Utils.Validators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Joystick.Client
{
    public class JoystickClient : IJoystickClient
    {
        private readonly JoystickClientConfig config;
        private readonly JoystickApiHttpService httpService;
        private readonly IContentJsonSerializer contentSerializer;

        public JoystickClient(
            JoystickClientConfig config,
            HttpClient httpClient = null,
            IContentJsonSerializer serializer = null)
        {
            if (config == null)
            {
                throw new JoystickArgumentException($"{nameof(config)} can't be null.");
            }

            config.Validate();

            this.config = config.Clone();
            this.httpService = new JoystickApiHttpService(httpClient ?? new HttpClient(), this.config);
            this.contentSerializer = serializer ?? new JoystickDefaultContentJsonSerializer();
        }

        public async Task<JoystickFullContent<TData>> GetFullContentAsync<TData>(string contentId, JoystickContentOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var settings = new GetContentSettings(options, typeof(TData));
            var responseBody = await this.GetSerializedFullContentAsync(contentId, settings, cancellationToken);
            return this.contentSerializer.Deserialize<JoystickFullContent<TData>>(responseBody);
        }

        public Task<JoystickFullContent<JObject>> GetFullContentAsync(string contentId, JoystickContentOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.GetFullContentAsync<JObject>(contentId, options, cancellationToken);
        }

        public async Task<TData> GetContentAsync<TData>(string contentId, JoystickContentOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var settings = new GetContentSettings(options, typeof(TData));
            var serializedContent = await this.GetSerializedFullContentAsync(contentId, settings, cancellationToken);
            return this.contentSerializer.Deserialize<JoystickBaseContent<TData>>(serializedContent).Data;
        }

        public Task<JObject> GetContentAsync(string contentId, JoystickContentOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.GetContentAsync<JObject>(contentId, options, cancellationToken);
        }

        public async Task<Dictionary<string, JoystickFullContent<TData>>> GetFullContentsAsync<TData>(IEnumerable<string> contentIds, JoystickContentOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var settings = new GetContentSettings(options, typeof(TData));
            var serializedContents = await this.GetSerializedFullContentsAsync(contentIds, settings, cancellationToken);

            return this.contentSerializer.Deserialize<Dictionary<string, JoystickFullContent<TData>>>(serializedContents);
        }

        public Task<Dictionary<string, JoystickFullContent<JObject>>> GetFullContentsAsync(IEnumerable<string> contentIds, JoystickContentOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.GetFullContentsAsync<JObject>(contentIds, options, cancellationToken);
        }

        public Task<Dictionary<string, JObject>> GetContentsAsync(IEnumerable<string> contentIds, JoystickContentOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.GetContentsAsync<JObject>(contentIds, options, cancellationToken);
        }

        public async Task<Dictionary<string, TData>> GetContentsAsync<TData>(IEnumerable<string> contentIds, JoystickContentOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var settings = new GetContentSettings(options, typeof(TData));
            var serializedContents = await this.GetSerializedFullContentsAsync(contentIds, settings);

            var contentDataDictionary = this.contentSerializer.Deserialize<Dictionary<string, JoystickBaseContent<TData>>>(serializedContents);
            var result = new Dictionary<string, TData>();
            foreach (var contentId in contentIds)
            {
                result[contentId] = contentDataDictionary[contentId].Data;
            }

            return result;
        }

        public Task PublishContentUpdateAsync(string contentId, JoystickPublishContentPayload payload, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(contentId))
            {
                throw new JoystickArgumentException($"{nameof(contentId)} can't be empty.");
            }

            if (payload == null)
            {
                throw new JoystickArgumentException($"{nameof(payload)} can't be null.");
            }

            payload.Validate();
            var requestBady = payload.MapToUpsertContentRequestBody(this.contentSerializer);

            return this.httpService.UpsertJsonContentAsync(contentId, requestBady, cancellationToken);
        }

        private async Task<string> GetSerializedFullContentAsync(string contentId, GetContentSettings settings, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(contentId))
            {
                throw new JoystickArgumentException($"{nameof(contentId)} can't be empty.");
            }

            var contentsJson = await this.httpService.GetJsonContentsAsync(new[] { contentId }, settings, cancellationToken);

            var partiallyDeserialized = new Dictionary<string, JToken>(StringComparer.OrdinalIgnoreCase);
            JsonConvert.PopulateObject(contentsJson, partiallyDeserialized);

            JsonContentsValidator.Validate(partiallyDeserialized);
            return partiallyDeserialized[contentId].ToString();
        }

        private async Task<string> GetSerializedFullContentsAsync(IEnumerable<string> contentIds, GetContentSettings settings, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (contentIds == null || !contentIds.Any())
            {
                throw new JoystickArgumentException($"{nameof(contentIds)} can't be empty.");
            }

            if (contentIds.Any(string.IsNullOrWhiteSpace))
            {
                throw new JoystickArgumentException($"{nameof(contentIds)} can't contain empty value.");
            }

            var contentsJson = await this.httpService.GetJsonContentsAsync(contentIds, settings, cancellationToken);
            var partiallyDeserialized = new Dictionary<string, JToken>(StringComparer.OrdinalIgnoreCase);
            JsonConvert.PopulateObject(contentsJson, partiallyDeserialized);

            JsonContentsValidator.Validate(partiallyDeserialized);

            return contentsJson;
        }
    }
}