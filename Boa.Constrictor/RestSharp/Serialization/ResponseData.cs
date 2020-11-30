using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Serialization class for response.
    /// </summary>
    public class ResponseData
    {
        #region Properties

        /// <summary>
        /// Response URI.
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// Response HTTP status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Response error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Response content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Response headers.
        /// </summary>
        public IList<ParameterData> Headers { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="response">Response object.</param>
        public ResponseData(IRestResponse response)
        {
            Uri = response.ResponseUri;
            StatusCode = response.StatusCode;
            ErrorMessage = response.ErrorMessage;
            Content = response.Content;
            Headers = ParameterData.GetParameterDataList(response.Headers);
        }

        #endregion
    }
}
