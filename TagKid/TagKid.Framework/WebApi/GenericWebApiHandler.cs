using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using TagKid.Framework.IoC;

namespace TagKid.Framework.WebApi
{
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

                var handler = DependencyContainer.Current.Resolve<IHttpHandler>();
                handler.Handle(request, response);

                return response;
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}
