using RestSharp;
using System;
using System.Collections.Generic;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Serialization class for request.
    /// </summary>
    public class RequestData
    {
        #region Properties

        /// <summary>
        /// Request method.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Request URI.
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// Request resource path.
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// Request parameters.
        /// </summary>
        public IList<ParameterData> Parameters { get; set; }

        /// <summary>
        /// Request body.
        /// </summary>
        public RequestBody Body { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="request">Request object.</param>
        public RequestData(IRestRequest request)
        {
            Method = request.Method.ToString();
            Uri = null;
            Resource = request.Resource;
            Parameters = ParameterData.GetParameterDataList(request.Parameters);
            Body = request.Body;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="client">RestSharp client.</param>
        /// <param name="request">Request object.</param>
        public RequestData(IRestRequest request, IRestClient client)
        {
            Method = request.Method.ToString();
            Uri = client.BuildUri(request);
            Resource = request.Resource;
            Parameters = ParameterData.GetParameterDataList(request.Parameters);
            Body = request.Body;
        }

        #endregion
    }
}
