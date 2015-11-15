using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TagKid.Framework.Owin.Configuration;

namespace TagKid.Framework.Hosting.Http
{
    public class SimpleHttpListener
    {
        private readonly IHttpRequestHandler _handler;
        private readonly HttpListener _listener;

        public SimpleHttpListener(IHttpRequestHandler handler)
        {
            _handler = handler;
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:8080/api/");
        }

        public async Task Run()
        {
            while (_listener.IsListening)
            {
                var ctx = await _listener.GetContextAsync();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                _handler.Handle(new SimpleHttpListenerRequest(ctx.Request), new SimpleHttpListenerResponse(ctx.Response));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }

        public void Start()
        {
            _listener.Start();
            Task.Run(() => { Run(); });
        }

        public void Stop()
        {
            _listener.Stop();
        }
    }

    class SimpleHttpListenerResponse : IHttpResponse
    {
        private readonly HttpListenerResponse _response;

        public SimpleHttpListenerResponse(HttpListenerResponse response)
        {
            _response = response;
        }

        public void SetHeader(string key, string value)
        {
            _response.Headers[key] = value;
        }

        public void SetContent(string json)
        {
            _response.StatusCode = 200;
            _response.ContentType = "application/json";

            var data = Encoding.UTF8.GetBytes(json);
            _response.OutputStream.Write(data, 0, data.Length);

            _response.Close();
        }
    }

    class SimpleHttpListenerRequest : IHttpRequest
    {
        private readonly HttpListenerRequest _request;
        private string _content;

        public SimpleHttpListenerRequest(HttpListenerRequest request)
        {
            _request = request;
        }

        public Uri Uri => _request.Url;

        public string Content
        {
            get
            {
                if (_content == null)
                {
                    using (var sr = new StreamReader(_request.InputStream))
                    {
                        _content = sr.ReadToEnd();
                    }
                }

                return _content;
            }
        }

        public HttpMethod Method
        {
            get
            {
                switch (_request.HttpMethod)
                {
                    case "POST":
                        return HttpMethod.Post;
                    case "GET":
                        return HttpMethod.Get;
                    case "PUT":
                        return HttpMethod.Put;
                    case "DELETE":
                        return HttpMethod.Delete;
                    default:
                        throw new NotSupportedException("Unsupported http method: " + _request.HttpMethod);
                }
            }
        }

        public string GetHeader(string key)
        {
            return _request.Headers[key];
        }

        public string GetParam(string key)
        {
            return _request.QueryString[key];
        }
    }
}
