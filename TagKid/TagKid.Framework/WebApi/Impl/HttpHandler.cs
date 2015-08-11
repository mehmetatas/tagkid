﻿using System;
using System.Net;
using System.Net.Http;
using System.Text;
using TagKid.Framework.Exceptions;
using TagKid.Framework.Json;

namespace TagKid.Framework.WebApi.Impl
{
    public class HttpHandler : IHttpHandler
    {
        private readonly IJsonSerializer _json;
        private readonly IRouteResolver _routeResolver;
        private readonly IParameterResolver _parameterResolver;
        private readonly IActionInvoker _invoker;

        public HttpHandler(IJsonSerializer json, IRouteResolver routeResolver, IParameterResolver parameterResolver, IActionInvoker invoker)
        {
            _json = json;
            _routeResolver = routeResolver;
            _parameterResolver = parameterResolver;
            _invoker = invoker;
        }

        public void Handle(HttpRequestMessage request, HttpResponseMessage response)
        {
            try
            {
                var routeContext = _routeResolver.Resolve(request);

                _parameterResolver.Resolve(routeContext);

                _invoker.InvokeAction(routeContext);

                SetResponse(response, routeContext.ReturnValue);
            }
            catch (Exception ex)
            {
                var err = ex as Error ?? Errors.Unknown;
                SetResponse(response, Response.Error(err));
            }
        }
        
        private void SetResponse(HttpResponseMessage response, object result)
        {
            var json = result == null
                ? String.Empty
                : _json.Serialize(result);

            response.Content = new StringContent(json, Encoding.UTF8, "application/json");

            response.StatusCode = HttpStatusCode.OK;
        }
    }
}
