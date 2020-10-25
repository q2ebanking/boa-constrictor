using Boa.Constrictor.Logging;
using System.Diagnostics;

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
    public abstract class AbstractWait<TValue>
    {
        #region Constants

        /// <summary>
        /// The default timeout value.
        /// Use this if an override timeout value is not provided,
        /// And if the actor does not have the SetTimeouts ability.
        /// </summary>
        public const int DefaultTimeout = 30;

        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="question">The question upon whose answer to wait.</param>
        /// <param name="condition">The expected condition for which to wait.</param>
        protected AbstractWait(IQuestion<TValue> question, ICondition<TValue> condition)
        {
            Question = question;
            Condition = condition;
            TimeoutSeconds = null;
            AdditionalSeconds = 0;
            ActualTimeout = -1;
            SuppressLogs = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The expected condition for which to wait.
        /// </summary>
        public ICondition<TValue> Condition { get; protected set; }

        /// <summary>
        /// The question upon whose answer to wait.
        /// </summary>
        public IQuestion<TValue> Question { get; protected set; }

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
        /// If the actor has the SetTimeouts ability, then the ability will be used to calculate timeouts.
        /// Otherwise, DefaultTimeout will be used.
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        protected int CalculateTimeout(IActor actor)
        {
            int timeout = actor.HasAbilityTo<SetTimeouts>()
                ? actor.Using<SetTimeouts>().CalculateTimeout(TimeoutSeconds)
                : TimeoutSeconds ?? DefaultTimeout;

            return timeout + AdditionalSeconds;
        }

        /// <summary>
        /// Waits until the question's answer value meets the condition.
        /// If the expected condition is not met within the time limit, then an exception is thrown.
        /// Returns the actual awaited value.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <returns></returns>
        protected TValue WaitForValue(IActor actor)
        {
            // Set variables
            TValue actual = default(TValue);
            bool satisfied = false;
            ActualTimeout = CalculateTimeout(actor);

            // Adjust log level if necessary (to avoid too many messages)
            LogSeverity original = actor.Logger.LowestSeverity;
            if (SuppressLogs && actor.Logger.LowestSeverity < LogSeverity.Warning)
                actor.Logger.LowestSeverity = LogSeverity.Warning;

            // Start the timer
            Stopwatch timer = new Stopwatch();
            timer.Start();

            try
            {
                // Repeatedly check if the condition is satisfied until the timeout is reached
                do
                {
                    actual = actor.AsksFor(Question);
                    satisfied = Condition.Evaluate(actual);
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
                throw new WaitingException<TValue>(this, actual);

            // Return the actual awaited value
            return actual;
        }

        /// <summary>
        /// Returns a description of the task.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = $"Wait until {Question} {Condition}";

            if (ActualTimeout >= 0)
                s += $" for up to {ActualTimeout}s";
            else if (TimeoutSeconds != null)
                s += $" for up to {TimeoutSeconds + AdditionalSeconds}s";

            return s;
        }

        #endregion
    }
}
