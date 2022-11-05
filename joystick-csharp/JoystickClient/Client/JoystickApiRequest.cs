using System.Collections.Generic;
using System.Net.Http;
using System.Xml.Linq;
using JoystickClient;
using JoystickClient.Client;
using Newtonsoft.Json.Linq;

namespace JoystickClient.Client
{

    internal class JoystickApiRequest
    {
        public HttpMethod Method { get; }

        public string Url { get; }

        public object BodyContent { get; set; }

        public string ContentType { get; set; }


        /// <summary>
        /// Query parameters
        /// </summary>
        public List<KeyValuePair<string, string>> QueryParams { get; private set; }

        /// <summary>
        /// Post parameters
        /// </summary>
        public List<KeyValuePair<string, string>> PostParams { get; private set; }

        /// <summary>
        /// Header parameters
        /// </summary>
        public List<KeyValuePair<string, string>> HeaderParams { get; private set; }

        /// <summary>
        /// Path parameters
        /// </summary>
        public List<KeyValuePair<string, string>> PathParams { get; private set; }

        public JoystickApiRequest(HttpMethod method, string path, List<KeyValuePair<string, string>> queryParams = null, object bodyContent = null,
            List<KeyValuePair<string, string>> headerParams = null, List<KeyValuePair<string, string>> postParams = null, List<KeyValuePair<string, string>> pathParams = null,
            string contentType = null)
        {
            Method = method;

            Url = path;
            ContentType = contentType;
            BodyContent = bodyContent;
            QueryParams = queryParams;
            PostParams = postParams;
            HeaderParams = headerParams;
            PathParams = pathParams;
        }

        public void AddHeaderParameter(string Key, string Value) => HeaderParams.Add(new KeyValuePair<string, string>(Key, Value));

        public void AddPostParameter(string Key, string Value) => PostParams.Add(new KeyValuePair<string, string>(Key, Value));

        public void AddQueryParameter(string Key, string Value) => QueryParams.Add(new KeyValuePair<string, string>(Key, Value));

        public void AddPathParameter(string Key, string Value) => PathParams.Add(new KeyValuePair<string, string>(Key, Value));
    }
}
