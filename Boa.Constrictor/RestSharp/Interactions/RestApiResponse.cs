using Boa.Constrictor.Screenplay;
using RestSharp;
using System;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Abstract parent class for the RestApiResponse interactions.
    /// Child classes differ on data deserialization.
    /// </summary>
    public abstract class AbstractRestApiResponse
    {
        #region Properties

        /// <summary>
        /// The REST request to call.
        /// </summary>
        public IRestRequest Request { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Protected constructor.
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        protected AbstractRestApiResponse(IRestRequest request) => Request = request;

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Executes the REST request using the given client.
        /// This method must be abstract because execution may or may not have deserialization.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        protected abstract IRestResponse Execute(IRestClient client);

        #endregion

        #region Methods

        /// <summary>
        /// Calls the REST request and returns the response.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <returns></returns>
        protected IRestResponse CallAs(IActor actor)
        {
            // Prepare variables
            var ability = actor.Using<CallRestApi>();
            IRestResponse response = null;
            DateTime? start = null;
            DateTime? end = null;

            try
            {
                // Make the request
                start = DateTime.UtcNow;
                response = Execute(ability.Client);
                end = DateTime.UtcNow;

                // Log the response code
                actor.Logger.Info($"Response code: {(int)response.StatusCode}");
            }
            finally
            {
                if (ability.CanDumpRequests())
                {
                    // Try to dump the request and the response
                    var data = new FullRestData(ability.Client, Request, response, start, end);
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
            $"REST response from calling {Request.Method} '{Request.Resource}'";

        #endregion
    }

    /// <summary>
    /// Calls the REST API given by the request spec and returns the response.
    /// The response is NOT parsed using a serializable object.
    /// Requires the CallRestApi ability.
    /// Automatically dumps requests and responses if the ability has a dumper.
    /// </summary>
    public class RestApiResponse : AbstractRestApiResponse, IQuestion<IRestResponse>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use public builder methods to construct the question.)
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        private RestApiResponse(IRestRequest request) : base(request) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        /// <returns></returns>
        public static RestApiResponse From(IRestRequest request) => new RestApiResponse(request);

        #endregion

        #region Methods

        /// <summary>
        /// Executes the request as the client.
        /// </summary>
        /// <param name="client">The REST client.</param>
        /// <returns></returns>
        protected override IRestResponse Execute(IRestClient client) => client.Execute(Request);

        /// <summary>
        /// Calls the REST request and returns the response.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <returns></returns>
        public IRestResponse RequestAs(IActor actor) => CallAs(actor);

        #endregion
    }

    /// <summary>
    /// Calls the REST API given by the request spec and returns the response.
    /// The response is parsed using the given data type.
    /// Requires the CallRestApi ability.
    /// Automatically dumps requests and responses if the ability has a dumper.
    /// </summary>
    /// <typeparam name="TData">The response data type for deserialization.</typeparam>
    public class RestApiResponse<TData> : AbstractRestApiResponse, IQuestion<IRestResponse<TData>>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use public builder methods to construct the question.)
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        private RestApiResponse(IRestRequest request) : base(request) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        /// <returns></returns>
        public static RestApiResponse<TData> From(IRestRequest request) => new RestApiResponse<TData>(request);

        #endregion

        #region Methods

        /// <summary>
        /// Executes the request as the client.
        /// </summary>
        /// <param name="client">The REST client.</param>
        /// <returns></returns>
        protected override IRestResponse Execute(IRestClient client) => client.Execute<TData>(Request);

        /// <summary>
        /// Calls the REST request and returns the response.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <returns></returns>
        public IRestResponse<TData> RequestAs(IActor actor) => (IRestResponse<TData>)CallAs(actor);

        #endregion
    }
}
