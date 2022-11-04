using JoystickClient;
using JoystickClient.Client;

namespace JoystickClient.Client;

internal class JoystickApiRequest
{
    public HttpMethod Method { get; }

    public string Url { get; }

    public object? BodyContent { get; set; }

    public string? ContentType { get; set; }


    /// <summary>
    /// Query parameters
    /// </summary>
    public List<(string Key, string Value)> QueryParams { get; private set; }

    /// <summary>
    /// Post parameters
    /// </summary>
    public List<(string Key, string Value)> PostParams { get; private set; }

    /// <summary>
    /// Header parameters
    /// </summary>
    public List<(string Key, string Value)> HeaderParams { get; private set; }

    /// <summary>
    /// Path parameters
    /// </summary>
    public List<(string Key, string Value)> PathParams { get; private set; }

    public JoystickApiRequest(HttpMethod method, string path, List<(string Key, string Value)>? queryParams = null, object? bodyContent = null,
        List<(string Key, string Value)>? headerParams = null, List<(string Key, string Value)>? postParams = null, List<(string Key, string Value)>? pathParams = null,
        string? contentType = null)
    {
        Method = method;

        Url = path;
        ContentType = contentType;
        BodyContent = bodyContent;
        QueryParams = queryParams ?? new();
        PostParams = postParams ?? new();
        HeaderParams = headerParams ?? new();
        PathParams = pathParams ?? new();
    }

    public void AddHeaderParameter(string Key, string Value) => HeaderParams.Add(new(Key, Value));

    public void AddPostParameter(string Key, string Value) => PostParams.Add(new(Key, Value));

    public void AddQueryParameter(string Key, string Value) => QueryParams.Add(new(Key, Value));

    public void AddPathParameter(string Key, string Value) => PathParams.Add(new(Key, Value));
}
