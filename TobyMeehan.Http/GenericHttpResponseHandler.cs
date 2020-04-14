using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TobyMeehan.Http
{
    class GenericHttpResponseHandler<T> : IHttpResponseHandler
    {
        public GenericHttpResponseHandler(Action<T> action)
        {
            _handler = action;
        }

        private Action<T> _handler;

        public async Task Handle(HttpResponseMessage response)
        {
            T item = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            _handler(item);
        }
    }
}
