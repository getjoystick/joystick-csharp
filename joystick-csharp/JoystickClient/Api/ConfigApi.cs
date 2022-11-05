using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JoystickClient.Client;
using JoystickClient.Interfaces;
using JoystickClient.Models;

namespace JoystickClient.Api
{

    internal sealed class ConfigApi : ApiBase, IConfigApi
    {
        private const string getConfigPath = "/config/{contentId}";

        public ConfigApi(IJoystickApiHttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<string> GetConfigAsync(string contentId, CancellationToken token = default)
        {
            ConfigValidate(contentId);

            var pathParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("contentId", contentId) };

            var request = CreateRequest(getConfigPath, HttpMethod.Get, pathParams: pathParams);
            var response = await CallApiAsync(request, token);

            return HandleResponse<string, ErrorResponseBody>(response);
        }

        public async Task<string> GetDynamicConfig(string contentId, string userId, IDictionary<string, object> parameters, CancellationToken token = default)
        {
            ConfigValidate(contentId);

            var pathParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("contentId", contentId) };
            var postBody = new GetDynamicConfigRequestBody() { UserId = userId, Parameters = parameters };

            var request = CreateRequest(getConfigPath, HttpMethod.Post, pathParams: pathParams, postBody: postBody);
            var response = await CallApiAsync(request, token);

            return HandleResponse<string, ErrorResponseBody>(response);
        }

        private static void ConfigValidate(string contentId)
        {
            if (string.IsNullOrEmpty(contentId))
                throw new JoystickApiException(HttpStatusCode.BadRequest, "Missing required parameter 'contentId'");
        }
    }
}