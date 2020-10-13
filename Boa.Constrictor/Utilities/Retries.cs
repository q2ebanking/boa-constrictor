using Boa.Constrictor.Logging;
using System;
using System.Threading;

namespace Boa.Constrictor.Utilities
{
    /// <summary>
    /// Provides helper methods to retry actions.
    /// </summary>
    public static class Retries
    {
        /// <summary>
        /// Attempts to avoid exceptions by automatically retrying web interactions.
        /// Rethrows the exception if the maximum number of attempts is exceeded.
        /// Why is this useful? Some Selenium WebDriver methods are known to be intermittently unreliable.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <typeparam name="TReturn">The interaction value return type.</typeparam>
        /// <param name="interaction">A function that performs the interaction and returns a value.</param>
        /// <param name="callName">A description for the interaction (for logging).</param>
        /// <param name="attempts">The maximum number of attempts before giving up.</param>
        /// <param name="delayMilliseconds">The milliseconds to wait before each retry.</param>
        /// <param name="logger">The logger. If null, no warning are logged.</param>
        public static TReturn RetryOnException<TException, TReturn>(
            Func<TReturn> interaction,
            string callName,
            int attempts = 3,
            int delayMilliseconds = 0,
            ILogger logger = null)
            where TException : Exception
        {
            TException exception;
            TReturn value = default(TReturn);
            int retry = 0;

            do
            {
                try
                {
                    exception = null;
                    value = interaction();
                }
                catch (TException e)
                {
                    if (++retry >= attempts)
                        throw;

                    if (logger != null)
                        logger.Warning($"Retrying {callName} call after {delayMilliseconds} ms because it threw {e.GetType()}");

                    Thread.Sleep(delayMilliseconds);
                    exception = e;
                }
            }
            while (exception != null && retry < attempts);

            return value;
        }
    }
}
