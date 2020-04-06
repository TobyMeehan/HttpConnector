using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TobyMeehan.Http
{
    public interface IHttpResponseHandler
    {
        Task Handle(HttpResponseMessage response);
    }
}
