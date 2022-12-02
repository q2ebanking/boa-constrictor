namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Represents a Boolean condition to evaluate.
    /// The condition takes in a generically-typed value.
    /// </summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    public interface ICondition<TValue>
    {
        /// <summary>
        /// Evaluates the condition for the given value.
        /// </summary>
        /// <param name="actual">The given value.</param>
        /// <returns></returns>
        bool Evaluate(TValue actual);
    }
}
