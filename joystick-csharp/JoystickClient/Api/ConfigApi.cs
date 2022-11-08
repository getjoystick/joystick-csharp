using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JoystickClient.Client;
using JoystickClient.Interfaces;
using JoystickClient.Models;
using JoystickClient.Models.ApiRequest;
using JoystickClient.Models.ApiResponse;

namespace JoystickClient.Api
{

    internal sealed class ConfigApi : ApiBase, IConfigApi
    {
        private const string getConfigPath = "config/{contentId}";
        private const string getConfigHashPath = "config/{contentId}/hash";
        private const string getConfigDynamicPath = "config/{contentId}/dynamic";
        private const string getConfigDynamicHashPath = "config/{contentId}/dynamic/hash";

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

        public async Task<GetDynamicConfigHashResponse> GetDynamicConfigHashAsync(string contentId, string userId, IDictionary<string, object> parameters, CancellationToken token = default)
        {
            ConfigValidate(contentId);

            var pathParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("contentId", contentId) };
            var postBody = new GetDynamicConfigRequestBody() { UserId = userId, Parameters = parameters };

            var request = CreateRequest(getConfigHashPath, HttpMethod.Post, pathParams: pathParams, postBody: postBody);
            var response = await CallApiAsync(request, token);

            return HandleResponse<GetDynamicConfigHashResponse, ErrorResponseBody>(response);
        }

        public async Task<GetVeryDynamicConfigResponse> GetVeryDynamicConfigAsync(string contentId, string userId, IDictionary<string, object> parameters, DynamicConfigResponseType responseType = DynamicConfigResponseType.Parsed, CancellationToken token = default)
        {
            ConfigValidate(contentId);

            var pathParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("contentId", contentId) };
            var postBody = new GetDynamicConfigRequestBody() { UserId = userId, Parameters = parameters };
            var queryParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("responseType", Convert.ToString(responseType, CultureInfo.InvariantCulture)) };

            var request = CreateRequest(getConfigDynamicPath, HttpMethod.Post, pathParams: pathParams, postBody: postBody, queryParams: queryParams);
            var response = await CallApiAsync(request, token);

            return HandleResponse<GetVeryDynamicConfigResponse, ErrorResponseBody>(response);
        }

        public async Task<GetDynamicConfigHashResponse> GetVeryDynamicConfigHashAsync(string contentId, string userId, IDictionary<string, object> parameters, CancellationToken token = default)
        {
            ConfigValidate(contentId);

            var pathParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("contentId", contentId) };
            var postBody = new GetDynamicConfigRequestBody() { UserId = userId, Parameters = parameters };

            var request = CreateRequest(getConfigDynamicHashPath, HttpMethod.Post, pathParams: pathParams, postBody: postBody);
            var response = await CallApiAsync(request, token);

            return HandleResponse<GetDynamicConfigHashResponse, ErrorResponseBody>(response);
        }

        private static void ConfigValidate(string contentId)
        {
            if (string.IsNullOrEmpty(contentId))
                throw new JoystickApiException(HttpStatusCode.BadRequest, "Missing required parameter 'contentId'");
        }
    }
}