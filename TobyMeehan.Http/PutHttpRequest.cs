using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TobyMeehan.Http
{
    public class PutHttpRequest : HttpRequestBase
    {
        public PutHttpRequest(HttpClient client, string uri, object data) : base(client, uri)
        {
            Data = data;
        }

        public object Data { get; }

        public override async Task SendAsync()
        {
            HttpResponseMessage response = await Client.PutAsJsonAsync(Uri, Data);

            await HandleResponse(response);
        }
    }
}
