using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TobyMeehan.Http
{
    class UnconditionalHttpResponseHandler<T> : IHttpResponseHandler
    {
        public UnconditionalHttpResponseHandler(Action<T, HttpStatusCode> action)
        {
            _handler = action;
        }

        private Action<T, HttpStatusCode> _handler;

        public async Task Handle(HttpResponseMessage response)
        {
            T item = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            _handler(item, response.StatusCode);
        }
    }
}
