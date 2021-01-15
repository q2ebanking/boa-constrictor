using Boa.Constrictor.Screenplay;
using System;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Should be used for any Screenplay REST API exceptions.
    /// </summary>
    public class RestApiException : ScreenplayException
    {
        #region Constructors

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public RestApiException() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public RestApiException(string message) : base(message) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="inner">The inner exception.</param>
        public RestApiException(string message, Exception inner) : base(message, inner) { }

        #endregion
    }
}
