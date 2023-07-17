using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Joystick.Client.Core;
using Joystick.Client.Exceptions;
using Joystick.Client.Models;
using Joystick.Client.Models.Api;
using Joystick.Client.Utils;
using Newtonsoft.Json;

namespace Joystick.Client.Services.Http
{
    internal class JoystickApiHttpService
    {
        private readonly HttpClient httpClient;
        private readonly JoystickClientConfig clientConfig;

        public JoystickApiHttpService(HttpClient httpClient, JoystickClientConfig clientConfig)
        {
            this.httpClient = httpClient;
            this.clientConfig = clientConfig;
        }

        public async Task<string> GetJsonContentsAsync(IEnumerable<string> contentIds, GetContentSettings settings, CancellationToken cancellationToken)
        {
            var requestUrl = UrlHelper.ConstructGetContentUrl(contentIds, settings.IsContentSerialized);

            var requestBody = this.clientConfig.MapToGetContentRequestBody();

            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            request.Headers.Add(Constants.ApiKeyHeaderName, this.clientConfig.ApiKey);
            request.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await this.SendRequestAsync(request, cancellationToken);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task UpsertJsonContentAsync(string contentId, UpsertContentRequestBody payloadJson, CancellationToken cancellationToken)
        {
            var requestUrl = UrlHelper.ConstructPutContentUrl(contentId);

            var request = new HttpRequestMessage(HttpMethod.Put, requestUrl);
            request.Headers.Add(Constants.ApiKeyHeaderName, this.clientConfig.ApiKey);
            request.Content = new StringContent(JsonConvert.SerializeObject(payloadJson), Encoding.UTF8, "application/json");

            await this.SendRequestAsync(request, cancellationToken);
        }

        private async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await this.httpClient.SendAsync(request, cancellationToken);

            try
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();

                return response;
            }
            catch (Exception e)
            {
                if ((int)response.StatusCode >= 500)
                {
                    throw new JoystickApiServerException(response.StatusCode, e);
                }

                if ((int)response.StatusCode >= 400)
                {
                    throw new JoystickApiBadRequestException(response.StatusCode, e);
                }

                throw new JoystickApiUnknownException(response.StatusCode, e);
            }
        }
    }
}
