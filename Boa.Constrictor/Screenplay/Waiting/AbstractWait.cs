using Boa.Constrictor.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Waits for a desired state.
    /// The desired state is expressed using a Question and an expected condition or several pairs of Questions and conditions.
    /// If the desired state does not happen within the time limit, then an exception is thrown.
    /// 
    /// If the Actor has the SetTimeouts Ability, then the Ability will be used to calculate timeouts.
    /// Otherwise, DefaultTimeout will be used.
    /// </summary>
    public abstract class AbstractWait
    {
        #region Constants

        /// <summary>
        /// The default timeout value.
        /// Use this if an override timeout value is not provided,
        /// And if the Actor does not have the SetTimeouts Ability.
        /// </summary>
        public const int DefaultTimeout = 30;

        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="evaluator"></param>
        protected AbstractWait(IConditionEvaluator evaluator)
        {
            ConditionEvaluators = new List<IConditionEvaluator> { evaluator };
            TimeoutSeconds = null;
            AdditionalSeconds = 0;
            ActualTimeout = -1;
            SuppressLogs = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// List of Conditions to evaluate.
        /// </summary>
        public List<IConditionEvaluator> ConditionEvaluators { get; protected set; }

        /// <summary>
        /// The timeout override in seconds.
        /// If null, use the standard timeout value.
        /// </summary>
        public int? TimeoutSeconds { get; protected set; }

        /// <summary>
        /// An additional amount to add to the timeout.
        /// </summary>
        public int AdditionalSeconds { get; protected set; }

        /// <summary>
        /// The actual timeout used.
        /// </summary>
        private int ActualTimeout { get; set; }

        /// <summary>
        /// If true, do not print log messages below "Warning" severity while waiting.
        /// This is set to true by default.
        /// </summary>
        public bool SuppressLogs { get; protected set; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// If the Actor has the SetTimeouts Ability, then the Ability will be used to calculate timeouts.
        /// Otherwise, DefaultTimeout will be used.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <returns></returns>
        protected int CalculateTimeout(IActor actor)
        {
            int timeout = actor.HasAbilityTo<SetTimeouts>()
                ? actor.Using<SetTimeouts>().CalculateTimeout(TimeoutSeconds)
                : TimeoutSeconds ?? DefaultTimeout;

            return timeout + AdditionalSeconds;
        }

        /// <summary>
        /// Evaluate the conditions.
        /// If all conditions in a group return true,
        /// this condition will be considered satisifed
        /// and this method will return true.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <returns></returns>
        protected bool EvaluateCondition(IActor actor)
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
        /// Separate sequential list of Conditions into groups of Conditions.
        /// Iterates over each condition. AND conditions are added to the current group.
        /// OR conditions are added to a new group. This group is considered the current group
        /// as new AND conditions are discovered until the next OR condition.
        /// </summary>
        /// <returns></returns>
        protected List<List<IConditionEvaluator>> ParseConditionGroups()
        {
            var groups = new List<List<IConditionEvaluator>>
                { new List<IConditionEvaluator>() };

            foreach (var pair in ConditionEvaluators)
            {
                if (pair.Operator == ConditionOperators.Or)
                    groups.Add(new List<IConditionEvaluator>());

                groups.Last().Add(pair);
            }

            return groups;
        }

        /// <summary>
        /// Waits until the Question's answer value meets the condition.
        /// If the expected condition is not met within the time limit, then an exception is thrown.
        /// Returns the actual awaited value.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <returns></returns>
        protected void WaitForValue(IActor actor)
        {
            // Set variables
            bool satisfied = false;
            ActualTimeout = CalculateTimeout(actor);

            // Adjust log level if necessary (to avoid too many messages)
            LogSeverity original = actor.Logger.LowestSeverity;
            if (SuppressLogs && actor.Logger.LowestSeverity < LogSeverity.Warning)
                actor.Logger.LowestSeverity = LogSeverity.Warning;

            // Start the timer
            var timer = new Stopwatch();
            timer.Start();

            try
            {
                // Repeatedly check if the condition is satisfied until the timeout is reached
                do
                {
                    satisfied = EvaluateCondition(actor);
                }
                while (!satisfied && timer.Elapsed.TotalSeconds < ActualTimeout);
            }
            finally
            {
                // Return the log level to normal, no matter what goes wrong
                if (SuppressLogs)
                    actor.Logger.LowestSeverity = original;

                // Stop the timer
                timer.Stop();
            }

            // Verify successful waiting
            if (!satisfied)
            {
                if (ConditionEvaluators.Count > 1)
                    throw new WaitingException(this, ConditionEvaluators.Select(c => c.Answer).ToList());
                else
                    throw ConditionEvaluators[0].WaitingException(this);
            }
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = $"wait until {ConditionEvaluators[0]}";

            for (int i = 1; i < ConditionEvaluators.Count; i++)
            {
                s += $" {ConditionEvaluators[i].Operator} {ConditionEvaluators[i]}";
            }

            if (ActualTimeout >= 0)
                s += $" for up to {ActualTimeout}s";
            else if (TimeoutSeconds != null)
                s += $" for up to {TimeoutSeconds + AdditionalSeconds}s";

            return s;
        }

        #endregion
    }
}
