using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TobyMeehan.Http
{
    public class PostContentHttpRequest : HttpRequestBase
    {
        internal PostContentHttpRequest(HttpClient client, string uri, HttpContent content) : base (client, uri)
        {
            Content = content;
        }

        public HttpContent Content { get; }

        public override async Task SendAsync()
        {
            HttpResponseMessage response = await Client.PostAsync(Uri, Content);

            await HandleResponse(response);
        }
    }
}
