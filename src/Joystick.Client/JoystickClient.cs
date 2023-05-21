using Joystick.Client.Core;
using Joystick.Client.Exceptions;
using Joystick.Client.Models;
using Joystick.Client.Models.Api;
using Joystick.Client.Utils;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Joystick.Client
{
    public class JoystickClient : IJoystickClient
    {
        private readonly JoystickClientConfig config;
        private readonly HttpClient httpClient;

        public JoystickClient(JoystickClientConfig config)
        {
            if (config?.ApiKey == null)
            {
                throw new ArgumentNullException(nameof(config.ApiKey));
            }

            this.config = config.Clone();
            this.httpClient = new HttpClient();
        }

        public JoystickClient(IOptions<JoystickClientConfig> config)
            : this(config.Value)
        {
        }

        public async Task<JoystickFullResponse<T>> GetContentAsyncV1<T>(string contentId, JoystickContentOptions options = null)
        {
            var responseBody = await this.GetContentJsonAsync(contentId);
            return this.TryToDeserializeContent<T>(responseBody);
        }

        public async Task<JoystickFullResponse<string>> GetContentAsyncV1(string contentId, JoystickContentOptions options = null)
        {
            var responseBody =
                await this.GetContentJsonAsync(contentId);

            var jsonContent = this.TryToDeserializeContent<JsonObject>(responseBody);

            return new JoystickFullResponse<string>()
            {
                Data = jsonContent.Data.ToJsonString(),
                Hash = jsonContent.Hash,
                Meta = jsonContent.Meta,
            };
        }

        public async Task<IJoystickContent> GetContentV2<T>(string contentId, JoystickContentOptions options = null) 
        {
            if (options != null)
            {
                if (options.Serialized && !options.FullResponse && !typeof(T).IsAssignableFrom(typeof(JoystickSerializedContent)))
                {
                    throw new ArgumentException($"Unsupported combination");
                }
            }


            if (options != null && options.FullResponse)
            {
                if (options.Serialized)
                {
                    return await this.GetContentAsyncV1(contentId, options);
                }

                return await this.GetContentAsyncV1<T>(contentId, options);
            }

            if (options != null && options.Serialized)
            {

                var full = await this.GetContentAsyncV1(contentId, options);
                return new JoystickSerializedContent()
                {
                    Data = full.Data,
                };
            }

            var content = await this.GetContentAsyncV1<T>(contentId, options);
            return (IJoystickContent)content.Data;
        }

        private JoystickFullResponse<TOutput> TryToDeserializeContent<TOutput>(string json)
        {
            try
            {
                var content = JsonSerializer.Deserialize<JoystickFullResponse<TOutput>>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return content;
            }
            catch (Exception ex)
            {
                throw new JoystickException($"Unable to deserialize content data to type {typeof(TOutput).Name}", ex);
            }
        }

        private async Task<string> GetContentJsonAsync(string contentId)
        {
            if (string.IsNullOrWhiteSpace(contentId))
            {
                throw new ArgumentException($"{nameof(contentId)} should not be empty");
            }

            var requestUrl = $"https://api.getjoystick.com/api/v1/config/{contentId}/dynamic";
            var requestBody = new GetContentRequest(this.config);

            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            request.Headers.Add(Constants.ApiKeyHeaderName, this.config.ApiKey);
            request.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await this.SendRequestAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
        {
            var response = await this.httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }

            throw (int)response.StatusCode >= 500 ? new JoystickApiServerException(response.StatusCode) :
                (int)response.StatusCode >= 400 ? new JoystickApiBadRequestException(response.StatusCode) :
                new JoystickApiUnknownException(response.StatusCode);
        }
    }
}