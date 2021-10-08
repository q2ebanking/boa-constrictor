namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// An inquiry made by the actor about the state of the system under test.
    /// It should return a value representing the inquired state.
    /// </summary>
    /// <typeparam name="TAnswer">The answer type.</typeparam>
    public interface IQuestion<TAnswer> : IInteraction
    {
        /// <summary>
        /// Asks the Question and returns an answer.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <returns></returns>
        TAnswer RequestAs(IActor actor);
    }
}
