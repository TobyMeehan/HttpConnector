using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TobyMeehan.Http
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Creates a new GET request with the specified uri.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uri">The uri the request is sent to.</param>
        /// <returns></returns>
        public static IHttpRequest Get(this HttpClient client, string uri)
        {
            return new GetHttpRequest(client, uri);
        }

        /// <summary>
        /// Creates a new POST request with the specified uri, which automatically formats the data as JSON.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uri">The uri the request is sent to.</param>
        /// <param name="data">The data to send.</param>
        /// <returns></returns>
        public static IHttpRequest Post(this HttpClient client, string uri, object data)
        {
            return new PostHttpRequest(client, uri, data);
        }

        /// <summary>
        /// Creates a new POST request with the specified uri and content.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uri">The uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns></returns>
        public static IHttpRequest PostHttpContent(this HttpClient client, string uri, HttpContent content)
        {
            return new PostContentHttpRequest(client, uri, content);
        }

        /// <summary>
        /// Creates a new PUT request with the specified uri, which automatically formats the data as JSON.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uri">The uri the request is sent to.</param>
        /// <param name="data">The data to send.</param>
        /// <returns></returns>
        public static IHttpRequest Put(this HttpClient client, string uri, object data)
        {
            return new PutHttpRequest(client, uri, data);
        }

        /// <summary>
        /// Creates a new PUT request with the specified uri and content.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uri">The uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns></returns>
        public static IHttpRequest PutHttpContent(this HttpClient client, string uri, HttpContent content)
        {
            return new PutContentHttpRequest(client, uri, content);
        }

        /// <summary>
        /// Creates a new DELETE request with the specified uri.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uri">The uri the request is sent to.</param>
        /// <returns></returns>
        public static IHttpRequest Delete(this HttpClient client, string uri)
        {
            return new DeleteHttpRequest(client, uri);
        }
    }
}
