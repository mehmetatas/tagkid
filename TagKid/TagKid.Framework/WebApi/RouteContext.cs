using System;
using System.Net.Http;
using TagKid.Framework.WebApi.Configuration;

namespace TagKid.Framework.WebApi
{
    public class RouteContext
    {
        public HttpRequestMessage Request { get; set; }
        public ServiceMapping Service { get; set; }
        public MethodMapping Method { get; set; }
        public string DefaultParameter { get; set; }
        public object[] Parameters { get; set; }
        public object ReturnValue { get; set; }
        public Exception Exception{ get; set; }
    }
}