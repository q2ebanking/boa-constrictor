using Boa.Constrictor.Screenplay;
using RestSharp;
using System;
using System.Net;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Gets a cookie from the REST client by name.
    /// </summary>
    public class RestCookie : AbstractBaseUrlHandler, IQuestion<Cookie>
    {
        #region Constructors

        /// <summary>
        /// Private Constructor.
        /// (Use public builder methods instead.)
        /// </summary>
        /// <param name="baseUrl">The base URL for the request.</param>
        /// <param name="name">The cookie name.</param>
        private RestCookie(string baseUrl, string name) :
            base(baseUrl)
        {
            CookieName = name;
            ExpirationMinutes = null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The cookie name.
        /// </summary>
        public string CookieName { get; private set; }

        /// <summary>
        /// If not null, sets the cookie expiration time to the current time plus the minutes provided.
        /// </summary>
        public int? ExpirationMinutes { get; private set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Public builder method to construct the question.
        /// </summary>
        /// <param name="baseUrl">The base URL for the request.</param>
        /// <param name="name">The cookie name.</param>
        /// <returns></returns>
        public static RestCookie Named(string baseUrl, string name) =>
            new RestCookie(baseUrl, name);

        /// <summary>
        /// Sets ExpirationMinutes.
        /// </summary>
        /// <param name="minutes">The minutes to add to the current time for resetting cookie expiration.</param>
        /// <returns></returns>
        public RestCookie AndResetExpirationTo(int? minutes)
        {
            ExpirationMinutes = minutes;
            return this;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the authentication token from a REST client.
        /// Throws a RestApiException if the cookie does not exist.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <returns></returns>
        public Cookie RequestAs(IActor actor)
        {
            IRestClient client = actor.Using<CallRestApi>().GetClient(BaseUrl);
            Cookie cookie = client.CookieContainer.GetCookies(new Uri(BaseUrl))[CookieName];

            if (cookie == null)
                throw new RestApiException($"REST client for '{BaseUrl}' did not contain cookie '{CookieName}'");

            if (ExpirationMinutes != null)
                cookie.Expires = DateTime.UtcNow.AddMinutes((int)ExpirationMinutes);

            return cookie;
        }

        /// <summary>
        /// Returns a description of the question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"REST cookie named '{CookieName}'";

        #endregion
    }
}
