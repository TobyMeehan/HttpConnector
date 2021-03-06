﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TobyMeehan.Http
{
    class OkHttpResponseHandler<T> : IHttpResponseHandler
    {
        public OkHttpResponseHandler(Action<T> handler)
        {
            _handler = handler;
        }

        private Action<T> _handler;

        public async Task Handle(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            T item = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            _handler(item);
        }
    }
}
