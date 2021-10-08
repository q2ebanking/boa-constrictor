using RestSharp;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Provides fluent builder methods for RestSharp interactions that use the default RestSharp Ability.
    /// </summary>
    public static class Rest
    {
        /// <summary>
        /// Builder method for RestApiDownload that uses the CallRestApi Ability.
        /// Recommended usage: `Actor.Calls`.
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        /// <param name="fileExtension">The extension for the file to download.</param>
        /// <returns></returns>
        public static RestApiDownload<CallRestApi> Download(IRestRequest request, string fileExtension = null) =>
            new RestApiDownload<CallRestApi>(request, fileExtension);

        /// <summary>
        /// Builder method for RestApiCall that uses the CallRestApi Ability and does not deserialize the response.
        /// Recommended usage: `Actor.Calls`.
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        /// <returns></returns>
        public static RestApiCall<CallRestApi> Request(IRestRequest request) =>
            new RestApiCall<CallRestApi>(request);

        /// <summary>
        /// Builder method for RestApiCall that uses the CallRestApi Ability and deserializes the response.
        /// Recommended usage: `Actor.Calls`.
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        /// <typeparam name="TData">The response data type for deserialization.</typeparam>
        /// <returns></returns>
        public static RestApiCall<CallRestApi, TData> Request<TData>(IRestRequest request) =>
            new RestApiCall<CallRestApi, TData>(request);
    }

    /// <summary>
    /// Provides fluent builder methods for RestSharp interactions that use type-specific RestSharp Abilities.
    /// </summary>
    public static class Rest<TAbility>
        where TAbility : IRestSharpAbility
    {
        /// <summary>
        /// Builder method for RestApiDownload that uses a generic IRestSharpAbility Ability.
        /// Recommended usage: `Actor.Calls`.
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        /// <param name="fileExtension">The extension for the file to download.</param>
        /// <returns></returns>
        public static RestApiDownload<TAbility> Download(IRestRequest request, string fileExtension = null) =>
            new RestApiDownload<TAbility>(request, fileExtension);

        /// <summary>
        /// Builder method for RestApiCall that uses a generic IRestSharpAbility Ability and does not deserialize the response.
        /// Recommended usage: `Actor.Calls`.
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        /// <returns></returns>
        public static RestApiCall<TAbility> Request(IRestRequest request) =>
            new RestApiCall<TAbility>(request);

        /// <summary>
        /// Builder method for RestApiCall that uses a generic IRestSharpAbility Ability and deserializes the response.
        /// Recommended usage: `Actor.Calls`.
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        /// <typeparam name="TData">The response data type for deserialization.</typeparam>
        /// <returns></returns>
        public static RestApiCall<TAbility, TData> Request<TData>(IRestRequest request) =>
            new RestApiCall<TAbility, TData>(request);
    }
}
