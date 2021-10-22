using Boa.Constrictor.Screenplay;
using RestSharp;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Calls the REST API given by the request spec and returns the response.
    /// The response is NOT parsed using a serializable object.
    /// Requires the applicable IRestSharpAbility Ability.
    /// Automatically dumps requests and responses if the Ability has a dumper.
    /// </summary>
    /// <typeparam name="TAbility">The RestSharp Ability type.</typeparam>
    public class RestApiCall<TAbility> : AbstractRestQuestion<TAbility, IRestResponse>
        where TAbility : IRestSharpAbility
    {
        #region Constructors

        /// <summary>
        /// Internal constructor.
        /// (Use the Rest class to construct the Question.)
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        internal RestApiCall(IRestRequest request) : base(request) { }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the request using the given client.
        /// </summary>
        /// <param name="client">The RestSharp client.</param>
        /// <returns></returns>
        protected override IRestResponse Execute(IRestClient client) => client.Execute(Request);

        /// <summary>
        /// Calls the REST request and returns the response.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <returns></returns>
        public override IRestResponse RequestAs(IActor actor) => CallRequest(actor);

        /// <summary>
        /// Checks if this restApiCall is equal to another restApiCall.
        /// </summary>
        /// <param name="obj">The other object.</param>
        public override bool Equals(object obj)
        {
            bool same = obj is RestApiCall<TAbility> restApiCall &&
                restApiCall.request.AlwaysMultipartFormData == obj.request.AlwaysMultipartFormData &&
                restApiCall.request.JsonSerializer == obj.request.JsonSerializer &&
                restApiCall.request.XmlSerializer == pbj.request.XmlSerializer &&
                restApiCall.request.AdvancedResponseWriter == obj.request.AdvancedResponseWriter &&
                restApiCall.request.ResponseWriter == obj.request.ResponseWriter &&
                restApiCall.request.Method == obj.request.Method &&
                restApiCall.request.Resource == obj.request.Resource &&
                restApiCall.request.RequestFormat == obj.request.RequestFormat &&
                restApiCall.request.RootElement == obj.request.RootElement &&
                restApiCall.request.DateFormat == obj.request.DateFormat &&
                restApiCall.request.XmlNamespace == obj.request.XmlNamespac &&
                restApiCall.request.Credentials == obj.request.Credentials &&
                restApiCall.request.Timeout == obj.request.Timeout &&
                restApiCall.request.ReadWriteTimeout == obj.request.ReadWriteTimeout &&
                restApiCall.request.Attempts == obj.request.Attempts &&
                restApiCall.request.UseDefaultCredentials == obj.request.UseDefaultCredentials
                
            return same
        }
        #endregion
    }

    /// <summary>
    /// Calls the REST API given by the request spec and returns the response.
    /// The response is parsed using the given data type.
    /// Requires the applicable IRestSharpAbility Ability.
    /// Automatically dumps requests and responses if the Ability has a dumper.
    /// </summary>
    /// <typeparam name="TAbility">The RestSharp Ability type.</typeparam>
    /// <typeparam name="TData">The data deserialization object type.</typeparam>
    public class RestApiCall<TAbility, TData> : AbstractRestQuestion<TAbility, IRestResponse<TData>>
        where TAbility : IRestSharpAbility
    {
        #region Constructors

        /// <summary>
        /// Internal constructor.
        /// (Use the Rest class to construct the Question.)
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        internal RestApiCall(IRestRequest request) : base(request) { }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the request using the given client.
        /// </summary>
        /// <param name="client">The RestSharp client.</param>
        /// <returns></returns>
        protected override IRestResponse Execute(IRestClient client) => client.Execute<TData>(Request);

        /// <summary>
        /// Calls the REST request and returns the response with deserialized data.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <returns></returns>
        public override IRestResponse<TData> RequestAs(IActor actor) => (IRestResponse<TData>)CallRequest(actor);

        #endregion
    }
}
