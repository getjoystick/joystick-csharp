using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JoystickClient.Client;
using JoystickClient.Core;
using JoystickClient.Interfaces;
using Newtonsoft.Json;

namespace JoystickClient.Api
{
    internal abstract class ApiBase
    {
        private IJoystickApiHttpClient _httpClient;

        protected ApiBase(IJoystickApiHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Handles response
        /// </summary>
        /// <typeparam name="TSuccess"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        /// <exception cref="JoystickApiException"></exception>
        protected static TSuccess HandleResponse<TSuccess, TError>(JoystickApiResponse response)
        {
            try
            {
                if ((int)response.StatusCode >= 400 || response.StatusCode == 0)
                {
                    var content = response.Content != null ?
                        (typeof(TError) == typeof(string) ? Convert.ChangeType(response.Content, typeof(TError)) : JsonConvert.DeserializeObject<TError>(response.Content))
                        : default;

                    throw new JoystickApiException(response.StatusCode, response.ErrorMessage, content);
                }

                return typeof(TSuccess) == typeof(string) ? (TSuccess)Convert.ChangeType(response.Content, typeof(TSuccess)) : JsonConvert.DeserializeObject<TSuccess>(response.Content);
            }
            catch (Exception e) when (!(e is JoystickApiException))
            {
                throw new JoystickApiException(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Creates request
        /// </summary>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <param name="queryParams"></param>
        /// <param name="postBody"></param>
        /// <param name="headerParams"></param>
        /// <param name="formParams"></param>
        /// <param name="pathParams"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        protected static JoystickApiRequest CreateRequest(string path, HttpMethod method,
            List<KeyValuePair<string, string>> queryParams = null, object postBody = null,
            List<KeyValuePair<string, string>> headerParams = null,
            List<KeyValuePair<string, string>> formParams = null,
            List<KeyValuePair<string, string>> pathParams = null, string contentType = null, bool useApiVersion = true)
        {
            var url = $"{Constants.ApiBasePath}/{(useApiVersion ? Constants.ApiVersion + "/" : string.Empty)}{path}";

            return new JoystickApiRequest(method, url, queryParams, postBody, headerParams, formParams, pathParams, contentType);
        }

        /// <summary>
        /// Calls api
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected Task<JoystickApiResponse> CallApiAsync(JoystickApiRequest request, CancellationToken cancellationToken = default)
        {
            return _httpClient.SendRequestAsync(request, cancellationToken);
        }
    }
}
