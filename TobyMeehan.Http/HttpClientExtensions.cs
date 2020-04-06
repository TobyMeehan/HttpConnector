using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TobyMeehan.Http
{
    public static class HttpClientExtensions
    {
        public static void Init(this HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static IHttpRequest Get(this HttpClient client, string uri)
        {
            return new GetHttpRequest(client, uri);
        }

        public static IHttpRequest Post(this HttpClient client, string uri, object data)
        {
            return new PostHttpRequest(client, uri, data);
        }

        public static IHttpRequest Put(this HttpClient client, string uri, object data)
        {
            return new PutHttpRequest(client, uri, data);
        }

        public static IHttpRequest Delete(this HttpClient client, string uri)
        {
            return new DeleteHttpRequest(client, uri);
        }
    }
}
