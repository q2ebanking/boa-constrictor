using Boa.Constrictor.Screenplay;
using RestSharp;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Handles the base URL for REST-based Screenplay interactions.
    /// </summary>
    public abstract class AbstractBaseUrlHandler
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="baseUrl">The base URL for the request.</param>
        public AbstractBaseUrlHandler(string baseUrl) => BaseUrl = baseUrl;

        #endregion

        #region Properties

        /// <summary>
        /// The base URL for the request.
        /// </summary>
        public string BaseUrl { get; protected set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the REST client object from the Screenplay actor's CallRestApi ability.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <param name="addIfMissing">If true, add a new client for the base URL if one does not already exists.</param>
        /// <returns></returns>
        public IRestClient GetClient(IActor actor, bool addIfMissing = true) =>
            actor.Using<CallRestApi>().GetClient(BaseUrl, addIfMissing);

        /// <summary>
        /// Returns a description of the question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => GetType().Name;

        #endregion
    }
}
