namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Adaptor interface for evaluating a condition.
    /// </summary>
    public interface IConditionAdaptor
    {
        /// <summary>
        /// Boolean operator associated with the Condition.
        /// </summary>
        Operators Operator { get; set; }

        /// <summary>
        /// Evaluate the Condition.
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        bool Evaluate(IActor actor);

        /// <summary>
        /// Return the most recent Answer.
        /// </summary>
        /// <returns></returns>
        string GetAnswer();
    }
}
