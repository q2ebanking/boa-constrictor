using System;

namespace Boa.Constrictor.Logging
{
    /// <summary>
    /// Prints messages to System.Console.
    /// Logging uses a class-level lock to avoid half-printed lines because the console is global.
    /// </summary>
    public class ConsoleLogger : AbstractLogger
    {
        #region Class Variables

        /// <summary>
        /// Thread synchronization lock object.
        /// The console is global, so this should be a class-level lock.
        /// </summary>
        private static readonly object ConsoleLock = new Object();

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="lowestSeverity">The lowest severity message to log.</param>
        public ConsoleLogger(LogSeverity lowestSeverity = LogSeverity.Trace) : base(lowestSeverity: lowestSeverity) { }

        #endregion

        #region Log Method Implementations

        /// <summary>
        /// Closes the logging stream.
        /// (No-op for the console.)
        /// </summary>
        public override void Close()
        {
            // No-op: the console needs no closing
        }

        /// <summary>
        /// Logs a basic message to the console after checking the lowest severity.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <param name="severity">The severity level (defaults to Info).</param>
        protected override void LogRaw(string message, LogSeverity severity = LogSeverity.Info)
        {
            lock (ConsoleLock)
                Console.WriteLine(MessageFormat.StandardTimestamp(message, severity));
        }

        #endregion
    }
}
