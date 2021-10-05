using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Waits for a desired state.
    /// The desired state is expressed using pairs of questions and expected conditions.
    /// If the desired state does not happen within the time limit, then an exception is thrown.
    /// 
    /// If the actor has the SetTimeouts ability, then the ability will be used to calculate timeouts.
    /// Otherwise, DefaultTimeout will be used.
    /// </summary>
    /// <typeparam name="TAnswer">The type of the question's answer value.</typeparam>
    public class Wait<TAnswer> : AbstractWait, ITask
    {
        #region Properties

        /// <summary>
        /// List of Conditions to evaluate.
        /// </summary>
        public List<IConditionAdaptor> ConditionList { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="question">The question upon whose answer to wait.</param>
        /// <param name="condition">The expected condition for which to wait.</param>
        private Wait(IQuestion<TAnswer> question, ICondition<TAnswer> condition) : base()
        {
            ConditionList = new List<IConditionAdaptor> { new ConditionWrapper<TAnswer>(question, condition) };
        }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="question">The question upon whose answer to wait.</param>
        /// <param name="condition">The expected condition for which to wait.</param>
        /// <returns></returns>
        public static Wait<TAnswer> Until(IQuestion<TAnswer> question, ICondition<TAnswer> condition) =>
            new Wait<TAnswer>(question, condition);

        /// <summary>
        /// Sets an override value for timeout seconds.
        /// </summary>
        /// <param name="seconds">The new timeout in seconds.</param>
        /// <returns></returns>
        public Wait<TAnswer> ForUpTo(int? seconds)
        {
            TimeoutSeconds = seconds;
            return this;
        }

        /// <summary>
        /// Adds an additional amount to the timeout.
        /// </summary>
        /// <param name="seconds">The seconds to add to the timeout.</param>
        /// <returns></returns>
        public Wait<TAnswer> ForAnAdditional(int seconds)
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
        public Wait<TAnswer> ButDontSuppressLogs()
        {
            SuppressLogs = false;
            return this;
        }

        /// <summary>
        /// Add a question and condition pair to the list of conditions to be evaluated with an And operator.
        /// </summary>
        /// <typeparam name="TOtherAnswer"></typeparam>
        /// <param name="question"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Wait<TAnswer> And<TOtherAnswer>(IQuestion<TOtherAnswer> question, ICondition<TOtherAnswer> condition)
        {
            ConditionList.Add(new ConditionWrapper<TOtherAnswer>(question, condition, Operators.And));

            return this;
        }

        /// <summary>
        /// Add a question and condition pair to the list of conditions to be evaluated with an Or operator.
        /// </summary>
        /// <typeparam name="TOtherAnswer"></typeparam>
        /// <param name="question"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Wait<TAnswer> Or<TOtherAnswer>(IQuestion<TOtherAnswer> question, ICondition<TOtherAnswer> condition)
        {
            ConditionList.Add(new ConditionWrapper<TOtherAnswer>(question, condition, Operators.Or));

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

        /// <summary>
        /// Separate sequential list of Conditions into groups of Conditions by Or operators.
        /// </summary>
        /// <returns></returns>
        protected List<List<IConditionAdaptor>> ParseConditionGroups()
        {
            var groups = new List<List<IConditionAdaptor>>
                { new List<IConditionAdaptor>() };

            foreach (var pair in ConditionList)
            {
                if (pair.Operator == Operators.Or)
                    groups.Add(new List<IConditionAdaptor>());

                groups.Last().Add(pair);
            }

            return groups;
        }

        /// <summary>
        /// Evaluate the conditions.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <returns></returns>
        protected override bool EvaluateCondition(IActor actor)
        {
            var groups = ParseConditionGroups();

            foreach (var group in groups)
            {
                bool satisfied = true;

                foreach (var condition in group)
                {
                    if (!condition.Evaluate(actor))
                    {
                        satisfied = false;
                        break;
                    }
                }

                if (satisfied)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Throw the waiting exception if condition is not satisfied
        /// </summary>
        protected override void ThrowWaitException()
        {
            throw new WaitingException(this, string.Join(", ", ConditionList.Select(c => c.GetAnswer())));
        }

        /// <summary>
        /// Returns a description of the task.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = $"Wait until {ConditionList[0]}";

            for (int i = 1; i < ConditionList.Count; i++)
            {
                s += $" {ConditionList[i].Operator} {ConditionList[i]}";
            }

            if (ActualTimeout >= 0)
                s += $" for up to {ActualTimeout}s";
            else if (TimeoutSeconds != null)
                s += $" for up to {TimeoutSeconds + AdditionalSeconds}s";

            return s;
        }

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
