namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// An executable Task that an Actor can perform.
    /// It should do one main thing, and it does not return any value.
    /// </summary>
    public interface ITask : IInteraction
    {
        /// <summary>
        /// Performs the Task.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        void PerformAs(IActor actor);
    }
}
