namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Waits for a desired state.
    /// The desired state is expressed using a Question and an expected condition.
    /// If the desired state does not happen within the time limit, then an exception is thrown.
    /// 
    /// If the Actor has the SetTimeouts Ability, then the Ability will be used to calculate timeouts.
    /// Otherwise, DefaultTimeout will be used.
    /// </summary>
    /// <typeparam name="TAnswer">The type of the Question's answer value.</typeparam>
    public class ValueAfterWaiting<TAnswer> : AbstractWait, IQuestion<TAnswer>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="evaluator">The expected condition for which to wait.</param>
        private ValueAfterWaiting(IConditionEvaluator evaluator) : base(evaluator) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="question">The Question upon whose answer to wait.</param>
        /// <param name="condition">The expected condition for which to wait.</param>
        /// <returns></returns>
        public static ValueAfterWaiting<TAnswer> Until(IQuestion<TAnswer> question, ICondition<TAnswer> condition)
        {
            var evaluator = new ConditionEvaluator<TAnswer>(question, condition);
            return new ValueAfterWaiting<TAnswer>(evaluator);
        }

        /// <summary>
        /// Sets an override value for timeout seconds.
        /// </summary>
        /// <param name="seconds">The new timeout in seconds.</param>
        /// <returns></returns>
        public ValueAfterWaiting<TAnswer> ForUpTo(int? seconds)
        {
            TimeoutSeconds = seconds;
            return this;
        }

        /// <summary>
        /// Adds an additional amount to the timeout.
        /// </summary>
        /// <param name="seconds">The seconds to add to the timeout.</param>
        /// <returns></returns>
        public ValueAfterWaiting<TAnswer> ForAnAdditional(int seconds)
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
        public ValueAfterWaiting<TAnswer> ButDontSuppressLogs()
        {
            SuppressLogs = false;
            return this;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Waits until the Question's answer value meets the condition.
        /// If the expected condition is not met within the time limit, then an exception is thrown.
        /// Returns the actual value after waiting.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <returns></returns>
        public TAnswer RequestAs(IActor actor)
        {
            WaitForValue(actor);
            return (TAnswer)ConditionEvaluators[0].Answer;
        }

        #endregion
    }

    /// <summary>
    /// Static builder class to help readability of fluent calls for ValueAfterWaiting.
    /// </summary>
    public static class ValueAfterWaiting
    {
        /// <summary>
        /// Constructs a ValueAfterWaiting Question.
        /// This variant allows "ValueAfterWaiting.Until" calls to avoid generic type specification.
        /// </summary>
        /// <typeparam name="TValue">The value type.</typeparam>
        /// <param name="question">The Question upon whose answer to wait.</param>
        /// <param name="condition">The expected condition for which to wait.</param>
        /// <returns></returns>
        public static ValueAfterWaiting<TValue> Until<TValue>(IQuestion<TValue> question, ICondition<TValue> condition) =>
            ValueAfterWaiting<TValue>.Until(question, condition);
    }
}
