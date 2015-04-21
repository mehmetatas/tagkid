using System;
using System.Linq;
using System.Net.Http;
using TagKid.Framework.Exceptions;
using TagKid.Framework.WebApi.Configuration;
using HttpMethod = TagKid.Framework.WebApi.Configuration.HttpMethod;

namespace TagKid.Framework.WebApi.Impl
{
    public class RouteResolver : IRouteResolver
    {
        public RouteContext Resolve(HttpRequestMessage request)
        {
            var segments = request.RequestUri.Segments;
            
            var indexOfApi = -1;
            for (var i = 0; i < segments.Length; i++)
            {
                if (segments[i] == "api/")
                {
                    indexOfApi = i;
                    break;
                }
            }

            if (indexOfApi < 0)
            {
                throw Errors.F_RouteResolvingError.ToException();
            }

            // api/service/method
            // api/service/method/defaultParam

            if (segments.Length < indexOfApi + 3 || segments.Length > indexOfApi + 4)
            {
                throw Errors.F_RouteResolvingError.ToException();
            }

            var serviceRoute = segments[indexOfApi + 1].Trim('/');
            var methodRoute = segments[indexOfApi + 2].Trim('/');
            string defaultParam = null;

            if (segments.Length == indexOfApi + 4)
            {
                defaultParam = segments[indexOfApi + 3].Trim('/');   
            }

            HttpMethod httpMethod;
            switch (request.Method.Method)
            {
                case "POST":
                    httpMethod = HttpMethod.Post;
                    break;
                case "GET":
                    httpMethod = HttpMethod.Get;
                    break;
                case "PUT":
                    httpMethod = HttpMethod.Put;
                    break;
                case "DELETE":
                    httpMethod = HttpMethod.Delete;
                    break;
                default:
                    throw new NotSupportedException("Unsupported http method: " + request.Method.Method);
            }
            
            var service = ServiceConfig.Current.ServiceMappings.SingleOrDefault(s => s.ServiceRoute == serviceRoute);

            if (service == null)
            {
                throw Errors.F_RouteResolvingError.ToException();
            }

            var method = service.MethodMappings.SingleOrDefault(m => m.MethodRoute == methodRoute && m.HttpMethod == httpMethod);

            if (method == null)
            {
                throw Errors.F_RouteResolvingError.ToException();
            }

            return new RouteContext
            {
                Service = service,
                Method = method,
                Request = request,
                DefaultParameter = defaultParam
            };
        }
    }
}