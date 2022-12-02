using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Boa.Constrictor.Logging
{
    /// <summary>
    /// Adds messages to a read-only list of strings.
    /// (It is useful for unit testing.)
    /// </summary>
    public class ListLogger : AbstractLogger
    {
        #region Constructors

        /// <summary>
        /// Basic constructor.
        /// Initializes the message list to be empty.
        /// </summary>
        public ListLogger(LogSeverity lowestSeverity = LogSeverity.Trace) :
            base(lowestSeverity)
        {
            InternalMessages = new List<string>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Internal message list.
        /// </summary>
        private List<string> InternalMessages { get; set; }

        /// <summary>
        /// Public-facing read-only message list.
        /// </summary>
        public IReadOnlyList<string> Messages => InternalMessages;

        #endregion

        #region Log Method Implementations

        /// <summary>
        /// Closes the logging stream.
        /// (No-op for the console.)
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public override void Close()
        {
            // No-op
        }

        /// <summary>
        /// Logs a basic message to the list after checking the lowest severity.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <param name="severity">The severity level (defaults to Info).</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        protected override void LogRaw(string message, LogSeverity severity = LogSeverity.Info)
        {
            InternalMessages.Add(MessageFormat.StandardTimestamp(message, severity));
        }

        #endregion
    }
}
