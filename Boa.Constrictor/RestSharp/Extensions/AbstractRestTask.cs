using Boa.Constrictor.Screenplay;
using RestSharp;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Abstract parent class for tasks that use the CallRestApi ability.
    /// It captures the REST client automatically.
    /// </summary>
    public abstract class AbstractRestTask : AbstractBaseUrlHandler, ITask
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="baseUrl">The base URL for the request.</param>
        public AbstractRestTask(string baseUrl) : base(baseUrl) { }

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Performs the task.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <param name="client">The REST client for executing requests.</param>
        public abstract void PerformAs(IActor actor, IRestClient client);

        #endregion

        #region Methods

        /// <summary>
        /// Performs the task.
        /// Internally calls PerformAs with the REST client from the CallRestApi ability.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        public void PerformAs(IActor actor) => PerformAs(actor, GetClient(actor));

        #endregion
    }
}
