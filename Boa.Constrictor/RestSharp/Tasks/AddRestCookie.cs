using Boa.Constrictor.Screenplay;
using RestSharp;
using System.Net;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Adds the cookie to the REST client for the desired base URL.
    /// </summary>
    public class AddRestCookie : AbstractRestTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="baseUrl">The base URL for the REST client.</param>
        /// <param name="cookie">The cookie to add.</param>
        private AddRestCookie(string baseUrl, Cookie cookie) : base(baseUrl) => Cookie = cookie;

        #endregion

        #region Properties

        /// <summary>
        /// The cookie to add.
        /// </summary>
        private Cookie Cookie { get; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="baseUrl">The base URL for the request.</param>
        /// <param name="cookie">The cookie to add.</param>
        /// <returns></returns>
        public static AddRestCookie Named(string baseUrl, Cookie cookie) => new AddRestCookie(baseUrl, cookie);

        #endregion

        #region Methods

        /// <summary>
        /// Adds the cookie to the REST client for the desired base URL.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <param name="client">The REST client.</param>
        /// <returns></returns>
        public override void PerformAs(IActor actor, IRestClient client) => client.CookieContainer.Add(Cookie);

        /// <summary>
        /// Returns a description of the task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Add cookie named '{Cookie.Name}' to the REST client";

        #endregion
    }
}
