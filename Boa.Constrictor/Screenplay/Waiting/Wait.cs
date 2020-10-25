namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Waits for a desired state.
    /// The desired state is expressed using a question and an expected condition.
    /// If the desired state does not happen within the time limit, then an exception is thrown.
    /// 
    /// If the actor has the SetTimeouts ability, then the ability will be used to calculate timeouts.
    /// Otherwise, DefaultTimeout will be used.
    /// </summary>
    /// <typeparam name="TValue">The type of the question's answer value.</typeparam>
    public class Wait<TValue> : AbstractWait<TValue>, ITask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="question">The question upon whose answer to wait.</param>
        /// <param name="condition">The expected condition for which to wait.</param>
        private Wait(IQuestion<TValue> question, ICondition<TValue> condition) :
            base(question, condition)
        { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="question">The question upon whose answer to wait.</param>
        /// <param name="condition">The expected condition for which to wait.</param>
        /// <returns></returns>
        public static Wait<TValue> Until(IQuestion<TValue> question, ICondition<TValue> condition) =>
            new Wait<TValue>(question, condition);

        /// <summary>
        /// Sets an override value for timeout seconds.
        /// </summary>
        /// <param name="seconds">The new timeout in seconds.</param>
        /// <returns></returns>
        public Wait<TValue> ForUpTo(int? seconds)
        {
            TimeoutSeconds = seconds;
            return this;
        }

        /// <summary>
        /// Adds an additional amount to the timeout.
        /// </summary>
        /// <param name="seconds">The seconds to add to the timeout.</param>
        /// <returns></returns>
        public Wait<TValue> ForAnAdditional(int seconds)
        {
            AdditionalSeconds = seconds;
            return this;
        }

        /// <summary>
        /// Sets the flag to suppress logs to false.
        /// All logs will be printed during waiting.
        /// This may generate lots of spam.
        /// </summary>
        /// <returns></returns>
        public Wait<TValue> ButDontSuppressLogs()
        {
            SuppressLogs = false;
            return this;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Waits until the question's answer value meets the condition.
        /// If the expected condition is not met within the time limit, then an exception is thrown.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <returns></returns>
        public void PerformAs(IActor actor) => WaitForValue(actor);

        #endregion
    }

    /// <summary>
    /// Static builder class to help readability of fluent calls for Wait.
    /// </summary>
    public static class Wait
    {
        /// <summary>
        /// Constructs a Wait task.
        /// This variant allows "Wait.Until" calls to avoid generic type specification.
        /// </summary>
        /// <typeparam name="TValue">The value type.</typeparam>
        /// <param name="question">The question upon whose answer to wait.</param>
        /// <param name="condition">The expected condition for which to wait.</param>
        /// <returns></returns>
        public static Wait<TValue> Until<TValue>(IQuestion<TValue> question, ICondition<TValue> condition) =>
            Wait<TValue>.Until(question, condition);
    }
}
