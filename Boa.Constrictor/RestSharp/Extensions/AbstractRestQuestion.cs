using Boa.Constrictor.Screenplay;
using RestSharp;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Abstract parent class for questions that use the CallRestApi ability.
    /// It captures the REST client automatically.
    /// </summary>
    /// <typeparam name="TAnswer">The answer type.</typeparam>
    public abstract class AbstractRestQuestion<TAnswer> : AbstractBaseUrlHandler, IQuestion<TAnswer>
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="baseUrl">The base URL for the request.</param>
        public AbstractRestQuestion(string baseUrl) : base(baseUrl) { }

        #endregion
        
        #region Abstract Methods

        /// <summary>
        /// Asks the question and returns the answer.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <param name="client">The REST client for executing requests.</param>
        /// <returns></returns>
        public abstract TAnswer RequestAs(IActor actor, IRestClient client);

        #endregion

        #region Methods

        /// <summary>
        /// Asks the question and returns the answer.
        /// Internally calls RequestAs with the REST client from the CallRestApi ability.
        /// </summary>
        /// <param name="actor">The screenplay actor.</param>
        /// <returns></returns>
        public TAnswer RequestAs(IActor actor) => RequestAs(actor, GetClient(actor));

        #endregion
    }
}
