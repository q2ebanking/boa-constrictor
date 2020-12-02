using RestSharp;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Provides fluent builder methods for RestSharp interactions.
    /// </summary>
    public static class Rest
    {
        /// <summary>
        /// Builder method for RestApiDownload that uses the CallRestApi ability.
        /// Recommended usage: `Actor.Calls`.
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        /// <param name="fileExtension">The extension for the file to download.</param>
        /// <returns></returns>
        public static RestApiDownload<CallRestApi> Download(IRestRequest request, string fileExtension = null) =>
            new RestApiDownload<CallRestApi>(request, fileExtension);

        /// <summary>
        /// Builder method for RestApiDownload that uses a generic IRestSharpAbility ability.
        /// Recommended usage: `Actor.Calls`.
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        /// <param name="fileExtension">The extension for the file to download.</param>
        /// <typeparam name="TAbility">The RestSharp Ability type.</typeparam>
        /// <returns></returns>
        public static RestApiDownload<TAbility> DownloadUsing<TAbility>(IRestRequest request, string fileExtension = null)
            where TAbility : IRestSharpAbility =>
            new RestApiDownload<TAbility>(request, fileExtension);

        /// <summary>
        /// Builder method for RestApiCall that uses the CallRestApi ability and does not deserialize the response.
        /// Recommended usage: `Actor.Calls`.
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        /// <returns></returns>
        public static RestApiCall<CallRestApi> Request(IRestRequest request) =>
            new RestApiCall<CallRestApi>(request);

        /// <summary>
        /// Builder method for RestApiCall that uses the CallRestApi ability and deserializes the response.
        /// Recommended usage: `Actor.Calls`.
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        /// <typeparam name="TData">The response data type for deserialization.</typeparam>
        /// <returns></returns>
        public static RestApiCall<CallRestApi, TData> Request<TData>(IRestRequest request) =>
            new RestApiCall<CallRestApi, TData>(request);

        /// <summary>
        /// Builder method for RestApiCall that uses a generic IRestSharpAbility ability ability and does not deserialize the response.
        /// Recommended usage: `Actor.Calls`.
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        /// <typeparam name="TAbility">The RestSharp Ability type.</typeparam>
        /// <returns></returns>
        public static RestApiCall<TAbility> RequestUsing<TAbility>(IRestRequest request)
            where TAbility : IRestSharpAbility =>
            new RestApiCall<TAbility>(request);

        /// <summary>
        /// Builder method for RestApiCall that uses a generic IRestSharpAbility ability ability and does not deserialize the response.
        /// Recommended usage: `Actor.Calls`.
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        /// <typeparam name="TAbility">The RestSharp Ability type.</typeparam>
        /// <typeparam name="TData">The response data type for deserialization.</typeparam>
        /// <returns></returns>
        public static RestApiCall<TAbility, TData> RequestUsing<TAbility, TData>(IRestRequest request)
            where TAbility : IRestSharpAbility =>
            new RestApiCall<TAbility, TData>(request);
    }
}
