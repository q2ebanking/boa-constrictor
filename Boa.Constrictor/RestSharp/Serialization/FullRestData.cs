using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Serialization class for all data.
    /// </summary>
    public class FullRestData
    {
        #region Properties

        /// <summary>
        /// Duration time data.
        /// </summary>
        public DurationData Duration { get; set; }

        /// <summary>
        /// Request data.
        /// </summary>
        public RequestData Request { get; set; }

        /// <summary>
        /// Response data.
        /// </summary>
        public ResponseData Response { get; set; }

        /// <summary>
        /// Request cookies.
        /// </summary>
        public IList<Cookie> Cookies { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="client">RestSharp client.</param>
        /// <param name="request">Request object.</param>
        /// <param name="response">Response object.</param>
        /// <param name="start">Request's start time.</param>
        /// <param name="end">Request's end time.</param>
        public FullRestData(
            IRestClient client,
            IRestRequest request = null,
            IRestResponse response = null,
            DateTime? start = null,
            DateTime? end = null)
        {
            Duration = new DurationData(start, end);
            Request = (request == null) ? null : new RequestData(request, client);
            Response = (response == null) ? null : new ResponseData(response);
            Cookies = GetCookieData(client);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Converts the REST client cookies to a serializable object.
        /// </summary>
        /// <param name="client">RestSharp client.</param>
        /// <returns></returns>
        private IList<Cookie> GetCookieData(IRestClient client)
        {
            // Linq cannot be used because CookieCollection does not use generic typing.
            // Thus, we suffer with the foreach loop below.

            IList<Cookie> cookies = new List<Cookie>();

            foreach (var c in client.CookieContainer.GetCookies(client.BaseUrl))
                cookies.Add((Cookie)c);

            return cookies;
        }

        #endregion
    }
}
