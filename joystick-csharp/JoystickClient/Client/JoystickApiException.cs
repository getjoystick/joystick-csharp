using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JoystickClient;
using JoystickClient.Client;

namespace JoystickClient.Client;

public class JoystickApiException : Exception
{
    /// <summary>
    /// Gets the HTTP status code
    /// </summary>    
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets the error response content
    /// </summary>  
    public object ResponseContent { get; }

    /// <summary>
    /// Gets the error message
    /// </summary>  
    public string ErrorMessage { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="JoystickApiException"/> class.
    /// </summary>
    internal JoystickApiException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="JoystickApiException"/> class.
    /// </summary>
    /// <param name="errorCode">HTTP status code.</param>
    /// <param name="message">Error message.</param>
    public JoystickApiException(HttpStatusCode httpStatusCode, string message) : base(message)
    {
        StatusCode = httpStatusCode;
        ErrorMessage = message;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JoystickApiException"/> class.
    /// </summary>
    /// <param name="errorCode">HTTP status code.</param>
    /// <param name="errorMessage">Error message.</param>
    /// <param name="errorContent">Error content.</param> 
    internal JoystickApiException(HttpStatusCode httpStatusCode, string errorMessage, object errorContent = default) : base(errorMessage)
    {
        StatusCode = httpStatusCode;
        ResponseContent = errorContent;
        ErrorMessage = errorMessage;
    }
}
