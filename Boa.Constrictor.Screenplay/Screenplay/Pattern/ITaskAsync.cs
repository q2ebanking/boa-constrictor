using System.Threading.Tasks;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// An asynchronous executable Task that an Actor can perform.
    /// It should do one main thing, and it does not return any value.
    /// </summary>
    public interface ITaskAsync : IInteraction
    {
        /// <summary>
        /// Performs the Task asynchronously.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        Task PerformAsAsync(IActor actor);
    }
}
