using RestSharp;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Should be used for any Screenplay REST API file download exceptions.
    /// </summary>
    public class RestApiDownloadException : RestApiException
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <param name="response">The response object.</param>
        public RestApiDownloadException(IRestRequest request, IRestResponse response) 
            : base("REST API file download failed")
        {
            Request = request;
            Response = response;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The request object.
        /// </summary>
        public IRestRequest Request { get; }

        /// <summary>
        /// The response object.
        /// </summary>
        public IRestResponse Response { get;  }

        #endregion
    }
}