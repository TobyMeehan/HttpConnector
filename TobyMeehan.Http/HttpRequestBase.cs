using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TobyMeehan.Http
{
    public abstract class HttpRequestBase : IHttpRequest
    {
        internal HttpRequestBase(HttpClient client, string uri)
        {
            Client = client;
            Uri = uri;
        }

        protected HttpClient Client { get; set; }
        protected string Uri { get; set; }

        protected IHttpResponseHandler OkResponseHandler;
        protected IHttpResponseHandler BadRequestResponseHandler;
        protected IHttpResponseHandler UnconditionalResponseHandler;
        protected Dictionary<HttpStatusCode, IHttpResponseHandler> GenericResponseHandlers = new Dictionary<HttpStatusCode, IHttpResponseHandler>();

        protected async Task HandleResponse(HttpResponseMessage response)
        {
            if (UnconditionalResponseHandler != null)
            {
                await UnconditionalResponseHandler.Handle(response);
            }

            if (GenericResponseHandlers.ContainsKey(response.StatusCode))
            {
                await GenericResponseHandlers[response.StatusCode].Handle(response);
            }
            else if (response.IsSuccessStatusCode)
            {
                if (OkResponseHandler != null)
                {
                    await OkResponseHandler.Handle(response);
                }
            }
            else
            {
                if (BadRequestResponseHandler != null)
                {
                    await BadRequestResponseHandler.Handle(response);
                }
            }
        }

        public IHttpRequest OnOK<T>(Action<T> action)
        {
            OkResponseHandler = new OkHttpResponseHandler<T>(action);
            return this;
        }

        public IHttpRequest OnBadRequest<T>(Action<T, HttpStatusCode, string> action)
        {
            BadRequestResponseHandler = new BadRequestHttpResponseHandler<T>(action);
            return this;
        }

        public IHttpRequest Always<T>(Action<T, HttpStatusCode> action)
        {
            UnconditionalResponseHandler = new UnconditionalHttpResponseHandler<T>(action);
            return this;
        }

        public IHttpRequest On<T>(HttpStatusCode statusCode, Action<T> action)
        {
            GenericResponseHandlers.Add(statusCode, new GenericHttpResponseHandler<T>(action));
            return this;
        }

        public abstract Task SendAsync();
    }
}
