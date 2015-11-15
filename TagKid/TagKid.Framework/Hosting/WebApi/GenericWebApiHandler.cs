using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using TagKid.Framework.IoC;

namespace TagKid.Framework.Hosting.WebApi
{
    [Obsolete("Use TagKid.Framework.Hosting.Owin")]
    internal class GenericWebApiHandler : DelegatingHandler
    {
        public GenericWebApiHandler(HttpConfiguration httpConfig)
        {
            InnerHandler = new HttpControllerDispatcher(httpConfig);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken).ContinueWith(responseToCompleteTask =>
            {
                var response = responseToCompleteTask.Result;

                var handler = DependencyContainer.Current.Resolve<IHttpRequestHandler>();
                handler.Handle(new WebApiHttpRequest(request), new WebApiHttpResponse(response));

                return response;
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}
