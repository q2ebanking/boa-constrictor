namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Interface that represents the evaluation of a Condition and the related Answer.
    /// </summary>
    public interface IConditionEvaluator
    {
        /// <summary>
        /// The Answer to a Question.
        /// </summary>
        object Answer { get; }

        /// <summary>
        /// Boolean operator associated with the Condition.
        /// </summary>
        ConditionOperators Operator { get; }

        /// <summary>
        /// Evaluate the Condition.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <returns></returns>
        bool Evaluate(IActor actor);

        /// <summary>
        /// Return the WaitingException caused by interaction.
        /// </summary>
        /// <param name="interaction"></param>
        /// <returns></returns>
        WaitingException WaitingException(AbstractWait interaction);
    }
}
