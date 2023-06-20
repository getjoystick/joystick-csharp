using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Joystick.Client.Core;
using Joystick.Client.Exceptions;
using Joystick.Client.Models.Internal.Api;
using Joystick.Client.Utils;
using Newtonsoft.Json;

namespace Joystick.Client.Services.Http
{
    internal class JoystickApiHttpService
    {
        private readonly HttpClient httpClient;

        public JoystickApiHttpService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetJsonContentsAsync(IEnumerable<string> contentIds, GetContentSettings settings)
        {
            var requestUrl = $"{Constants.BaseReadUrl}/v1/combine/?dynamic=true&c={JsonConvert.SerializeObject(contentIds)}";

            if (settings.IsContentSerialized)
            {
                requestUrl += "&responseType=serialized";
            }

            var requestBody = settings.ClientConfig.MapToGetContentRequestBody();

            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            request.Headers.Add(Constants.ApiKeyHeaderName, settings.ClientConfig.ApiKey);
            request.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await this.SendRequestAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
        {
            var response = await this.httpClient.SendAsync(request);

            try
            {
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
