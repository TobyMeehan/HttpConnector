using Newtonsoft.Json;
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
            HttpContent content = new StringContent(JsonConvert.SerializeObject(Data), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Client.PutAsync(Uri, content);

            await HandleResponse(response);
        }
    }
}
