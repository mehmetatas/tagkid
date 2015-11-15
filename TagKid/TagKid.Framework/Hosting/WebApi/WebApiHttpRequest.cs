using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using HttpMethod = TagKid.Framework.Owin.Configuration.HttpMethod;

namespace TagKid.Framework.Hosting.WebApi
{
    [Obsolete("Use TagKid.Framework.Hosting.Owin")]
    class WebApiHttpRequest : IHttpRequest
    {
        private readonly HttpRequestMessage _request;
        private string _content;
        private IDictionary<string, string> _queryParams; 

        public WebApiHttpRequest(HttpRequestMessage request)
        {
            _request = request;
        }

        public Uri Uri => _request.RequestUri;

        public string Content => _content ?? (_content = _request.Content.ReadAsStringAsync().Result);

        public HttpMethod Method
        {
            get
            {
                switch (_request.Method.Method)
                {
                    case "POST":
                        return HttpMethod.Post;
                    case "GET":
                        return HttpMethod.Get;
                    case "PUT":
                        return HttpMethod.Put;
                    case "DELETE":
                        return HttpMethod.Delete;
                    default:
                        throw new NotSupportedException("Unsupported http method: " + _request.Method.Method);
                }
            }
        }

        public string GetHeader(string key)
        {
            IEnumerable<string> values;

            return _request.Headers.TryGetValues(key, out values)
                ? values.FirstOrDefault()
                : null;
        }

        public string GetParam(string key)
        {
            if (_queryParams == null)
            {
                _queryParams = _request.GetQueryNameValuePairs()
                       .ToDictionary(kv => kv.Key, kv => kv.Value,
                            StringComparer.OrdinalIgnoreCase);
            }

            string value;

            return _queryParams.TryGetValue(key, out value)
                ? value
                : null;
        }
    }
}
