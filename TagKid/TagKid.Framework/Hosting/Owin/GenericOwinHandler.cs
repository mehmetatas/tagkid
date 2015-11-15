﻿using System.Threading.Tasks;
using Microsoft.Owin;

namespace TagKid.Framework.Hosting.Owin
{
    public class GenericOwinHandler: IOwinHandler
    {
        private readonly IHttpRequestHandler _httpRequestHandler;

        public GenericOwinHandler(IHttpRequestHandler httpRequestHandler)
        {
            _httpRequestHandler = httpRequestHandler;
        }

        public Task HandleRequestAsync(IOwinContext ctx)
        {
            return _httpRequestHandler.Handle(new OwinHttpRequest(ctx.Request), new OwinHttpResponse(ctx.Response));
        }
    }
}
