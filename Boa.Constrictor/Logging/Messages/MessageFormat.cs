using System;

namespace Boa.Constrictor.Logging
{
    /// <summary>
    /// Provides static methods for formatting log messages.
    /// </summary>
    public static class MessageFormat
    {
        #region Static Methods

        /// <summary>
        /// Formats a message with a timestamp and severity level prefix.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <param name="severity">The severity level as a string.</param>
        /// <returns></returns>
        public static string StandardTimestamp(string message, string severity)
        {
            string ts = DateTime.UtcNow.ToString("u" /*"yyyy-MM-dd HH:mm:ss"*/);
            return $"{ts} [{severity.ToUpper()}] {message}";
        }

        /// <summary>
        /// Formats a message with a timestamp and severity level prefix.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <param name="severity">The log severity level.</param>
        /// <returns></returns>
        public static string StandardTimestamp(string message, LogSeverity severity) =>
            StandardTimestamp(message, severity.ToString());

        #endregion
    }
}
