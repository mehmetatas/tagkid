using System;
using TagKid.Framework.Owin.Configuration;

namespace TagKid.Framework.Hosting
{
    public interface IHttpRequest
    {
        Uri Uri { get; }
        string Content { get; }
        HttpMethod Method { get; }
        string GetHeader(string key);
        string GetParam(string key);
    }
}
