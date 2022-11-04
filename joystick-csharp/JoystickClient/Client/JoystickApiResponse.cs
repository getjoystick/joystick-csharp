using System.Net;
using System.Net.Mime;
using System.Reflection.PortableExecutable;
using JoystickClient;
using JoystickClient.Client;

namespace JoystickClient.Client;

internal class JoystickApiResponse
{
    public byte[]? ContentRawBytes { get; }
    public string Content => ContentRawBytes != null ? System.Text.Encoding.UTF8.GetString(ContentRawBytes) : null;
    public string ContentType { get; }
    public string ErrorMessage { get; }
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.OK;
    public IDictionary<string, string>? Headers { get; }

    public JoystickApiResponse(HttpStatusCode statusCode, IDictionary<string, string> headers = default,
        byte[] rawBytes = default,
        string contentType = default,
        string errorMessage = default)
    {
        StatusCode = statusCode;
        Headers = headers;
        ContentRawBytes = rawBytes;
        ContentType = contentType;
        ErrorMessage = errorMessage;
    }
}

