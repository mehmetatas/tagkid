using System;

namespace TagKid.Framework.WebApi.Configuration
{
    public enum HttpMethod
    {
        Get,
        Post,
        Put,
        Delete
    }

    public static class HttpMethodExtensions
    {
        public static System.Net.Http.HttpMethod ToSystemNetHttpMethod(this HttpMethod httpMethod)
        {
            switch (httpMethod)
            {
                case HttpMethod.Get:
                    return System.Net.Http.HttpMethod.Get;
                case HttpMethod.Post:
                    return System.Net.Http.HttpMethod.Post;
                case HttpMethod.Put:
                    return System.Net.Http.HttpMethod.Put;
                case HttpMethod.Delete:
                    return System.Net.Http.HttpMethod.Delete;
                default:
                    throw new ArgumentOutOfRangeException("httpMethod");
            }
        }
    }
}