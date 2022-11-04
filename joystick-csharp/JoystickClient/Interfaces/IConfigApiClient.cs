using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoystickClient.Client;

namespace JoystickClient.Interfaces;

public interface IConfigApi
{
    /// <summary>
    /// Retrieves config by content Id. 
    /// </summary>
    /// <exception cref="Client.JoystickApiException">Thrown when fails to make API call</exception>
    /// <param name="contentId"></param>
    /// <param name="token"></param>
    /// <returns>Task of String</returns>
    Task<string> GetConfigAsync(string contentId, CancellationToken token = default);

    /// <summary>
    /// Retrieves dynamic config by content Id. 
    /// </summary>
    /// <exception cref="Client.JoystickApiException">Thrown when fails to make API call</exception>
    /// <param name="contentId"></param>
    /// <param name="userId"></param>
    /// <param name="parameters">Any other information to segment your user on</param>
    /// <param name="token"></param>
    /// <returns>Task of String</returns>
    Task<string> GetDynamicConfig(string contentId, string userId, IDictionary<string, object> parameters, CancellationToken token = default);
}
