using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Utilities;
using RestSharp;
using System;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Calls the REST API given by the request spec and returns the response.
    /// The response is parsed using the given data type.
    /// (If no response body is expected, use "object" as the type.)
    /// Requires the CallRestApi ability.
    /// Automatically dumps requests and responses if the ability has a dumper.
    /// </summary>
    /// <typeparam name="TData">The response data type for deserialization.</typeparam>
    public class RestApiResponse<TData> : AbstractBaseUrlHandler, IQuestion<IRestResponse<TData>>
        where TData : new()
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use public builder methods to construct the question.)
        /// </summary>
        /// <param name="baseUrl">The base URL for the request.</param>
        /// <param name="request">The REST request to call.</param>
        private RestApiResponse(string baseUrl, IRestRequest request) :
            base(baseUrl) => Request = request;

        #endregion

        #region Properties

        /// <summary>
        /// The REST request to call.
        /// </summary>
        private IRestRequest Request { get; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="baseUrl">The base URL for the request.</param>
        /// <param name="request">The REST request to call.</param>
        /// <returns></returns>
        public static RestApiResponse<TData> From(string baseUrl, IRestRequest request) =>
            new RestApiResponse<TData>(baseUrl, request);

        #endregion

        #region Methods

        /// <summary>
        /// Calls the REST request and returns the response.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <returns></returns>
        public IRestResponse<TData> RequestAs(IActor actor)
        {
            // Get ability objects
            var ability = actor.Using<CallRestApi>();
            var client = ability.GetClient(BaseUrl);

            // Prepare response variables
            IRestResponse<TData> response = null;
            DateTime? start = null;
            DateTime? end = null;
            
            try
            {
                // Make the request
                start = DateTime.UtcNow;
                response = client.Execute<TData>(Request);
                end = DateTime.UtcNow;

                // Log the response code
                actor.Logger.Info($"Response code: {(int)response.StatusCode}");
            }
            finally
            {
                if (ability.CanDumpRequests())
                {
                    // Try to dump the request and the response
                    var data = new FullRestData(client, Request, response, start, end);
                    string path = ability.RequestDumper.Dump(data);
                    actor.Logger.Info($"Dumped request to: {path}");
                }
                else
                {
                    // Warn that the request will not be dumped
                    actor.Logger.Debug("Request will not be dumped");
                }
            }

            // Return the response object
            return response;
        }

        /// <summary>
        /// Returns a description of the question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"REST response from calling {Request.Method} '{Urls.Combine(BaseUrl, Request.Resource)}'";

        #endregion
    }

    /// <summary>
    /// Provides a non-type-generic builder method for RestApiResponse.
    /// </summary>
    public static class RestApiResponse
    {
        /// <summary>
        /// Constructs the question using "object" as the type.
        /// This builder allows callers to avoid the type generic templating.
        /// </summary>
        /// <param name="baseUrl">The base URL for the request.</param>
        /// <param name="request">The REST request to call.</param>
        /// <returns></returns>
        public static RestApiResponse<object> From(string baseUrl, IRestRequest request) =>
            RestApiResponse<object>.From(baseUrl, request);
    }
}
