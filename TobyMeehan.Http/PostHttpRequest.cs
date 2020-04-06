using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TobyMeehan.Http
{
    public class PostHttpRequest : HttpRequestBase
    {
        internal PostHttpRequest(HttpClient client, string uri, object data) : base(client, uri)
        {
            Data = data;
        }

        public object Data { get; }

        public override async Task SendAsync()
        {
            HttpResponseMessage response = await Client.PostAsJsonAsync(Uri, Data);

            await HandleResponse(response);
        }
    }
}
