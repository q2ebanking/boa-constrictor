using Boa.Constrictor.Dumping;
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
    /// </summary>
    /// <typeparam name="TData">The response data type for deserialization.</typeparam>
    public class RestApiResponse<TData> : AbstractRestQuestion<IRestResponse<TData>>
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
            base(baseUrl)
        {
            Request = request;
            OutputDir = null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The REST request to call.
        /// </summary>
        private IRestRequest Request { get; }

        /// <summary>
        /// The output directory for logging.
        /// </summary>
        private string OutputDir { get; set; }

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

        /// <summary>
        /// Sets the output directory for logging requests.
        /// </summary>
        /// <param name="outputDir"></param>
        /// <returns></returns>
        public RestApiResponse<TData> LoggedTo(string outputDir)
        {
            OutputDir = outputDir;
            return this;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Calls the REST request and returns the response.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <param name="client">The REST client.</param>
        /// <returns></returns>
        public override IRestResponse<TData> RequestAs(IActor actor, IRestClient client)
        {
            IRestResponse<TData> response = null;
            DateTime? start = null;
            DateTime? end = null;
            
            try
            {
                start = DateTime.UtcNow;
                response = client.Execute<TData>(Request);
                end = DateTime.UtcNow;

                actor.Logger.Info($"Response code: {(int)response.StatusCode}");
            }
            finally
            {
                if (OutputDir == null)
                {
                    actor.Logger.Debug("Request will not be logged because no output directory was provided");
                }
                else
                {
                    var data = new FullRestData(client, Request, response, start, end);
                    string path = new JsonDumper("Request Dumper", OutputDir, "Request").Dump(data);
                    actor.Logger.Info($"Logged request to: {path}");
                }
            }

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
}
