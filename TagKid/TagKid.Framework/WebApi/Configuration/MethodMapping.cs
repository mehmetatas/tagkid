using System.Reflection;

namespace TagKid.Framework.WebApi.Configuration
{
    public class MethodMapping
    {
        public string MethodRoute { get; set; }
        public MethodInfo Method { get; set; }
        public HttpMethod HttpMethod { get; set; }
    }
}