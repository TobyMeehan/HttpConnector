﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TobyMeehan.Http
{
    class BadRequestHttpResponseHandler<T> : IHttpResponseHandler
    {
        public BadRequestHttpResponseHandler(Action<T, HttpStatusCode, string> handler)
        {
            _handler = handler;
        }

        private Action<T, HttpStatusCode, string> _handler;

        public async Task Handle(HttpResponseMessage response)
        {
            T item = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            _handler(item, response.StatusCode, response.ReasonPhrase);
        }
    }
}
