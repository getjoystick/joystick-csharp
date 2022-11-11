using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JoystickClient.Client;
using JoystickClient.Models.ApiResponse;

namespace JoystickClient.Interfaces
{

    public interface IConfigApi
    {
        /// <summary>
        /// Retrieves config by content Id. 
        /// </summary>
        /// <exception cref="JoystickApiException">Thrown when fails to make API call</exception>
        /// <param name="contentId"></param>
        /// <param name="token"></param>
        /// <returns>Task of String</returns>
        Task<string> GetConfigAsync(string contentId, CancellationToken token = default);

        /// <summary>
        /// Retrieves dynamic config by content Id. 
        /// </summary>
        /// <exception cref="JoystickApiException">Thrown when fails to make API call</exception>
        /// <param name="contentId"></param>
        /// <param name="userId"></param>
        /// <param name="parameters">Any other information to segment your user on</param>
        /// <param name="token"></param>
        /// <returns>Task of String</returns>
        Task<string> GetDynamicConfig(string contentId, string userId, IDictionary<string, object> parameters, CancellationToken token = default);

        /// <summary>
        /// Retrieves dynamic config hash by content Id. 
        /// </summary>
        /// <exception cref="JoystickApiException">Thrown when fails to make API call</exception>
        /// <param name="contentId"></param>
        /// <param name="userId"></param>
        /// <param name="parameters">Any other information to segment your user on</param>
        /// <param name="token"></param>
        /// <returns>Task of GetDynamicConfigHashResponse</returns>
        Task<GetDynamicConfigHashResponse> GetDynamicConfigHashAsync(string contentId, string userId, IDictionary<string, object> parameters, CancellationToken token = default);

        /// <summary>
        /// Retrieves dynamic config with meta wrapper by content Id.
        /// </summary>
        /// <exception cref="JoystickApiException">Thrown when fails to make API call</exception>
        /// <param name="contentId"></param>
        /// <param name="userId"></param>
        /// <param name="parameters">Any other information to segment your user on</param>
        /// <param name="responseType"></param>
        /// <param name="token"></param>
        /// <returns>Task of GetDynamicConfigHashResponse</returns>
        Task<GetVeryDynamicConfigResponse> GetVeryDynamicConfigAsync(string contentId, string userId, IDictionary<string, object> parameters, 
            DynamicConfigResponseType responseType = DynamicConfigResponseType.Parsed, CancellationToken token = default);

        /// <summary>
        /// Retrieves dynamic config hash with wrapper by content Id.
        /// </summary>
        /// <exception cref="JoystickApiException">Thrown when fails to make API call</exception>
        /// <param name="contentId"></param>
        /// <param name="userId"></param>
        /// <param name="parameters">Any other information to segment your user on</param>    
        /// <param name="token"></param>
        /// <returns>Task of GetDynamicConfigHashResponse</returns>
        Task<GetDynamicConfigHashResponse> GetVeryDynamicConfigHashAsync(string contentId, string userId, IDictionary<string, object> parameters, CancellationToken token = default);
    }
}
