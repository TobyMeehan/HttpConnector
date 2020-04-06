using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TobyMeehan.Http
{
    public class GetHttpRequest : HttpRequestBase
    {
        internal GetHttpRequest(HttpClient client, string uri) : base(client, uri)
        {
        }

        public override async Task SendAsync()
        {
            HttpResponseMessage response = await Client.GetAsync(Uri);

            await HandleResponse(response);
        }
    }
}
