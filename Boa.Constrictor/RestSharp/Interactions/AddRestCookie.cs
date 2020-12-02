using Boa.Constrictor.Screenplay;
using System.Net;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Adds the cookie to the REST client for the desired base URL.
    /// </summary>
    public class AddRestCookie : ITask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="cookie">The cookie to add.</param>
        private AddRestCookie(Cookie cookie) => Cookie = cookie;

        #endregion

        #region Properties

        /// <summary>
        /// The cookie to add.
        /// </summary>
        public Cookie Cookie { get; private set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="cookie">The cookie to add.</param>
        /// <returns></returns>
        public static AddRestCookie Named(Cookie cookie) => new AddRestCookie(cookie);

        #endregion

        #region Methods

        /// <summary>
        /// Adds the cookie to the REST client for the desired base URL.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <returns></returns>
        public void PerformAs(IActor actor) => actor.Using<CallRestApi>().Client.CookieContainer.Add(Cookie);

        /// <summary>
        /// Returns a description of the task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Add cookie named '{Cookie.Name}' to the REST client";

        #endregion
    }
}
