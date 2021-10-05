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
    /// <typeparam name="TAnswer">The type of the question's answer value.</typeparam>
    public class ValueAfterWaiting<TAnswer> : AbstractWait, IQuestion<TAnswer>
    {
        #region Properties

        /// <summary>
        /// The answer to return upon waiting.
        /// </summary>
        private TAnswer Answer { get; set; }

        /// <summary>
        /// The expected condition for which to wait.
        /// </summary>
        private ICondition<TAnswer> Condition { get; set; }

        /// <summary>
        /// The question upon whose answer to wait.
        /// </summary>
        private IQuestion<TAnswer> Question { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="question">The question upon whose answer to wait.</param>
        /// <param name="condition">The expected condition for which to wait.</param>
        private ValueAfterWaiting(IQuestion<TAnswer> question, ICondition<TAnswer> condition) : base()
        {
            Condition = condition;
            Question = question;
        }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="question">The question upon whose answer to wait.</param>
        /// <param name="condition">The expected condition for which to wait.</param>
        /// <returns></returns>
        public static ValueAfterWaiting<TAnswer> Until(IQuestion<TAnswer> question, ICondition<TAnswer> condition) =>
            new ValueAfterWaiting<TAnswer>(question, condition);

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
        /// Waits until the question's answer value meets the condition.
        /// If the expected condition is not met within the time limit, then an exception is thrown.
        /// Returns the actual value after waiting.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <returns></returns>
        public TAnswer RequestAs(IActor actor)
        {
            WaitForValue(actor);
            return Answer;
        }

        /// <summary>
        /// Evaluate the condition.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <returns></returns>
        protected override bool EvaluateCondition(IActor actor)
        {
            Answer = actor.AsksFor(Question);
            return Condition.Evaluate(Answer);
        }

        /// <summary>
        /// Throw the waiting exception if condition is not satisfied
        /// </summary>
        protected override void ThrowWaitException()
        {
            throw new WaitingException(this, Answer.ToString());
        }

        /// <summary>
        /// Returns a description of the task.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = $"wait until {Question} {Condition}";

            if (ActualTimeout >= 0)
                s += $" for up to {ActualTimeout}s";
            else if (TimeoutSeconds != null)
                s += $" for up to {TimeoutSeconds + AdditionalSeconds}s";

            return s;
        }

        #endregion
    }

    /// <summary>
    /// Static builder class to help readability of fluent calls for ValueAfterWaiting.
    /// </summary>
    public static class ValueAfterWaiting
    {
        /// <summary>
        /// Constructs a ValueAfterWaiting question.
        /// This variant allows "ValueAfterWaiting.Until" calls to avoid generic type specification.
        /// </summary>
        /// <typeparam name="TValue">The value type.</typeparam>
        /// <param name="question">The question upon whose answer to wait.</param>
        /// <param name="condition">The expected condition for which to wait.</param>
        /// <returns></returns>
        public static ValueAfterWaiting<TValue> Until<TValue>(IQuestion<TValue> question, ICondition<TValue> condition) =>
            ValueAfterWaiting<TValue>.Until(question, condition);
    }
}
