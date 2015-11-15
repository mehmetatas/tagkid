using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace TagKid.Framework.Hosting.WebApi
{
    [Obsolete("Use TagKid.Framework.Hosting.Owin")]
    class WebApiHttpResponse : IHttpResponse
    {
        private readonly HttpResponseMessage _response;

        public WebApiHttpResponse(HttpResponseMessage response)
        {
            _response = response;
        }

        public void SetHeader(string key, string value)
        {
            _response.Headers.Add(key, value);
        }

        public void SetContent(string json)
        {
            _response.StatusCode = HttpStatusCode.OK;
            _response.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}