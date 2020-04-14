using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TobyMeehan.Http
{
    /// <summary>
    /// Interface representing an HTTP request.
    /// </summary>
    public interface IHttpRequest
    {
        /// <summary>
        /// Adds a handler for a success status code to the request.
        /// </summary>
        /// <typeparam name="T">Type of expected result data.</typeparam>
        /// <param name="action">Action to execute.</param>
        /// <returns></returns>
        IHttpRequest OnOK<T>(Action<T> action);

        /// <summary>
        /// Adds a handler for a non-success status code to the request.
        /// </summary>
        /// <typeparam name="T">Type of expected result data.</typeparam>
        /// <param name="action">Action to execute.</param>
        /// <returns></returns>
        IHttpRequest OnBadRequest<T>(Action<T, HttpStatusCode, string> action);

        /// <summary>
        /// Adds a handler which will always be invoked, regardless of the response state.
        /// </summary>
        /// <typeparam name="T">Type of expected result data.</typeparam>
        /// <param name="action">Action to execute.</param>
        IHttpRequest Always<T>(Action<T, HttpStatusCode> action);

        /// <summary>
        /// Adds a generic handler which will be invoked for the specified status code.
        /// </summary>
        /// <typeparam name="T">Type of expected result data.</typeparam>
        /// <param name="statusCode">Status code which will invoke the handler.</param>
        /// <param name="action">Action to execute.</param>
        /// <returns></returns>
        IHttpRequest On<T>(HttpStatusCode statusCode, Action<T> action);

        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <returns></returns>
        Task SendAsync();
    }
}
