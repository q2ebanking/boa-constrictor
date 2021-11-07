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



        // The commented code should be used as the basis for:
        // https://github.com/q2ebanking/boa-constrictor/issues/196

        //     /// <summary>
        //     /// Checks if this restApiCall is equal to another restApiCall.
        //     /// </summary>
        //     /// <param name="obj">The other object.</param>
        //     public override bool Equals(object obj)
        //     {
        //         bool same = obj is RestApiCall<TAbility> restApiCall &&
        //             restApiCall.request.AlwaysMultipartFormData == obj.request.AlwaysMultipartFormData &&
        //             restApiCall.request.JsonSerializer == obj.request.JsonSerializer &&
        //             restApiCall.request.XmlSerializer == pbj.request.XmlSerializer &&
        //             restApiCall.request.AdvancedResponseWriter == obj.request.AdvancedResponseWriter &&
        //             restApiCall.request.ResponseWriter == obj.request.ResponseWriter &&
        //             restApiCall.request.Method == obj.request.Method &&
        //             restApiCall.request.Resource == obj.request.Resource &&
        //             restApiCall.request.RequestFormat == obj.request.RequestFormat &&
        //             restApiCall.request.RootElement == obj.request.RootElement &&
        //             restApiCall.request.DateFormat == obj.request.DateFormat &&
        //             restApiCall.request.XmlNamespace == obj.request.XmlNamespace &&
        //             restApiCall.request.Credentials == obj.request.Credentials &&
        //             restApiCall.request.Timeout == obj.request.Timeout &&
        //             restApiCall.request.ReadWriteTimeout == obj.request.ReadWriteTimeout &&
        //             restApiCall.request.Attempts == obj.request.Attempts &&
        //             restApiCall.request.UseDefaultCredentials == obj.request.UseDefaultCredentials &&
        //             restApiCall.request.OnBeforeRequest == obj.request.OnBeforeRequest &&
        //             restApiCall.request.Body == obj.request.Body &&
        //             restApiCall.request.AllowedDecompressionMethods.length == obj.request.AllowedDecompressionMethods.length &&
        //             restApiCall.request.Files.length == obj.request.Files.length &&
        //             restApiCall.request.Parameters.length == obj.request.Parameters.length

        //         if (same) {
        //             for(int i = 0; i < restApiCall.request.AllowedDecompressionMethods.length; i++) {
        //                 same = same && restApiCall.request.AllowedDecompressionMethods[i].Equals(obj.request.AllowedDecompressionMethods[i])
        //             }

        //             for(int i = 0; i < restApiCall.request.Files.length; i++) {
        //                 same = same && restApiCall.request.Files[i].Equals(obj.request.Files[i])
        //             }

        //             for(int i = 0; i < restApiCall.request.Parameters.length; i++) {
        //                 same = same && restApiCall.request.Parameters[i].Equals(obj.request.Parameters[i])
        //             }
        //         }

        //         return same
        //     }

        //     /// <summary>
        //     /// Gets a unique hash code for this RestApiCall.
        //     /// </summary>
        //     /// <returns></returns>
        //     public override int GetHashCode()
        //     {
        //         HashCode.Combine(
        //             GetType(), 
        //             request.AlwaysMultipartFormData, 
        //             request.Method,
        //             request.Body,
        //             request.Timeout,
        //             request.Attempts,
        //             request.RootElement)
        //     }

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
