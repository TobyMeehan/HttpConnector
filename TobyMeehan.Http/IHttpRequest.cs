using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TobyMeehan.Http
{
    public interface IHttpRequest
    {
        IHttpRequest OnOK<T>(Action<T> action);
        IHttpRequest OnBadRequest<T>(Action<T, HttpStatusCode, string> action);
        IHttpRequest Always<T>(Action<T, HttpStatusCode> action);
        Task SendAsync();
    }
}
