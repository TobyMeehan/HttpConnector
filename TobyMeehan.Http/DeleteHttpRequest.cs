using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TobyMeehan.Http
{
    public class DeleteHttpRequest : HttpRequestBase
    {
        public DeleteHttpRequest(HttpClient client, string uri) : base(client, uri)
        {
        }

        public override async Task SendAsync()
        {
            HttpResponseMessage response = await Client.DeleteAsync(Uri);

            await HandleResponse(response);
        }
    }
}
