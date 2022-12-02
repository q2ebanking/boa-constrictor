using System.Threading.Tasks;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// An asynchronous inquiry made by the Actor about the state of the system under test.
    /// It should return a value representing the inquired state.
    /// </summary>
    /// <typeparam name="TAnswer">The answer type.</typeparam>
    public interface IQuestionAsync<TAnswer> : IInteraction
    {
        /// <summary>
        /// Asks the Question and returns an answer asynchronously.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <returns></returns>
        Task<TAnswer> RequestAsAsync(IActor actor);
    }
}
