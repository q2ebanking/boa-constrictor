namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Waits for a desired state.
    /// The desired state is expressed using pairs of Questions and expected conditions.
    /// If the desired state does not happen within the time limit, then an exception is thrown.
    /// 
    /// Additional Questions and expected conditions can be added using boolean operators (and, or).
    /// Conditions are evaluated sequentially in 'and' groups, separate by 'or'.
    /// 
    /// If the Actor has the SetTimeouts Ability, then the Ability will be used to calculate timeouts.
    /// Otherwise, DefaultTimeout will be used.
    /// </summary>
    public class Wait : AbstractWait, ITask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="evaluator">The condition to wait upon until satisfied.</param>
        private Wait(IConditionEvaluator evaluator) : base(evaluator) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question.
        /// </summary>
        /// <param name="question">The Question upon whose answer to wait.</param>
        /// <param name="condition">The expected condition for which to wait.</param>
        /// <returns></returns>
        public static Wait Until<TAnswer>(IQuestion<TAnswer> question, ICondition<TAnswer> condition)
        {
            var evaluator = new ConditionEvaluator<TAnswer>(question, condition);
            return new Wait(evaluator);
        }

        /// <summary>
        /// Add a Question and condition pair to the list of conditions to be evaluated with an And operator.
        /// </summary>
        /// <typeparam name="TAnswer"></typeparam>
        /// <param name="question"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Wait And<TAnswer>(IQuestion<TAnswer> question, ICondition<TAnswer> condition)
        {
            ConditionEvaluators.Add(new ConditionEvaluator<TAnswer>(question, condition, ConditionOperators.And));

            return this;
        }

        /// <summary>
        /// Add a Question and condition pair to the list of conditions to be evaluated with an Or operator.
        /// </summary>
        /// <typeparam name="TAnswer"></typeparam>
        /// <param name="question"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Wait Or<TAnswer>(IQuestion<TAnswer> question, ICondition<TAnswer> condition)
        {
            ConditionEvaluators.Add(new ConditionEvaluator<TAnswer>(question, condition, ConditionOperators.Or));

            return this;
        }

        /// <summary>
        /// Sets an override value for timeout seconds.
        /// </summary>
        /// <param name="seconds">The new timeout in seconds.</param>
        /// <returns></returns>
        public Wait ForUpTo(int? seconds)
        {
            TimeoutSeconds = seconds;
            return this;
        }

        /// <summary>
        /// Adds an additional amount to the timeout.
        /// </summary>
        /// <param name="seconds">The seconds to add to the timeout.</param>
        /// <returns></returns>
        public Wait ForAnAdditional(int seconds)
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
        public Wait ButDontSuppressLogs()
        {
            SuppressLogs = false;
            return this;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Waits until the Question's answer value meets the condition.
        /// If the expected condition is not met within the time limit, then an exception is thrown.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <returns></returns>
        public void PerformAs(IActor actor) => WaitForValue(actor);

        #endregion
    }
}
