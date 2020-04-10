using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TobyMeehan.Http
{
    public class PutContentHttpRequest : HttpRequestBase
    {
        internal PutContentHttpRequest(HttpClient client, string uri, HttpContent content) : base(client, uri)
        {
            Content = content;
        }

        public HttpContent Content { get; set; }

        public override async Task SendAsync()
        {
            HttpResponseMessage response = await Client.PutAsync(Uri, Content);

            await HandleResponse(response);
        }
    }
}
