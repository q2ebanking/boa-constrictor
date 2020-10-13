using RestSharp;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Should be used for any Screenplay REST API file download exceptions.
    /// </summary>
    public class RestApiDownloadException : RestApiException
    {
        #region Constructors

        public RestApiDownloadException(IRestRequest request, IRestResponse response) 
            : base("REST API file download failed")
        {
            Request = request;
            Response = response;
        }

        #endregion

        #region Properties

        public IRestRequest Request { get; }

        public IRestResponse Response { get;  }

        #endregion
    }
}