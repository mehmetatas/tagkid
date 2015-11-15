using System.Reflection;

namespace TagKid.Framework.Owin.Configuration
{
    public class MethodMapping
    {
        public string MethodRoute { get; set; }
        public MethodInfo Method { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public bool NoAuth { get; set; }
    }
}