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

        public RestApiException() { }
        public RestApiException(string message) : base(message) { }
        public RestApiException(string message, Exception inner) : base(message, inner) { }

        #endregion
    }
}
