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
        /// Returns the type name of the interaction.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => GetType().Name;

        #endregion
    }
}
