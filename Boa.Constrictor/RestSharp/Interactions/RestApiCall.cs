using Boa.Constrictor.Screenplay;
using RestSharp;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Calls the REST API given by the request spec and returns the response.
    /// The response is NOT parsed using a serializable object.
    /// Requires the applicable IRestSharpAbility ability.
    /// Automatically dumps requests and responses if the ability has a dumper.
    /// </summary>
    /// <typeparam name="TAbility">The RestSharp ability type.</typeparam>
    public class RestApiCall<TAbility> : AbstractRestQuestion<TAbility, object, IRestResponse>
        where TAbility : IRestSharpAbility
    {
        #region Constructors

        /// <summary>
        /// Internal constructor.
        /// (Use the Rest class to construct the question.)
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        internal RestApiCall(IRestRequest request) : base(request) { }

        #endregion

        #region Methods

        /// <summary>
        /// Calls the REST request and returns the response.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <returns></returns>
        public override IRestResponse RequestAs(IActor actor) => CallRequest(actor);

        #endregion
    }

    /// <summary>
    /// Calls the REST API given by the request spec and returns the response.
    /// The response is parsed using the given data type.
    /// Requires the applicable IRestSharpAbility ability.
    /// Automatically dumps requests and responses if the ability has a dumper.
    /// </summary>
    /// <typeparam name="TAbility">The RestSharp ability type.</typeparam>
    /// <typeparam name="TData">The response data type for deserialization.</typeparam>
    public class RestApiCall<TAbility, TData> : AbstractRestQuestion<TAbility, TData, IRestResponse<TData>>
        where TAbility : IRestSharpAbility
    {
        #region Constructors

        /// <summary>
        /// Internal constructor.
        /// (Use the Rest class to construct the question.)
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        internal RestApiCall(IRestRequest request) : base(request) { }

        #endregion

        #region Methods

        /// <summary>
        /// Calls the REST request and returns the response with deserialized data.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <returns></returns>
        public override IRestResponse<TData> RequestAs(IActor actor) => (IRestResponse<TData>)CallRequest(actor);

        #endregion
    }
}
