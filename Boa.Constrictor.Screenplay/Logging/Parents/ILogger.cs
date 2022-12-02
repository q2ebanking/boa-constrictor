namespace Boa.Constrictor.Logging
{
    /// <summary>
    /// A logging interface for the Screenplay Pattern.
    /// It provides abstraction for generic logging.
    /// </summary>
    public interface ILogger
    {
        #region Properties

        /// <summary>
        /// The lowest severity message to log.
        /// </summary>
        LogSeverity LowestSeverity { get; set; }

        #endregion

        #region Log Methods

        /// <summary>
        /// Closes the logging stream.
        /// </summary>
        void Close();

        /// <summary>
        /// Logs a basic message.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <param name="severity">The severity level (defaults to Info).</param>
        void Log(string message, LogSeverity severity = LogSeverity.Info);

        /// <summary>
        /// Logs an artifact.
        /// The artifact must be saved to a file, like a screenshot image or a JSON dump.
        /// </summary>
        /// <param name="type">The name for the type of artifact.</param>
        /// <param name="path">The file path to the artifact.</param>
        void LogArtifact(string type, string path);

        #endregion

        #region Log-by-Level Methods

        /// <summary>
        /// Logs a message with Trace severity.
        /// </summary>
        /// <param name="message">The message text.</param>
        void Trace(string message);

        /// <summary>
        /// Logs a message with Debug severity.
        /// </summary>
        /// <param name="message">The message text.</param>
        void Debug(string message);

        /// <summary>
        /// Logs a message with Info severity.
        /// </summary>
        /// <param name="message">The message text.</param>
        void Info(string message);

        /// <summary>
        /// Logs a message with Warning severity.
        /// </summary>
        /// <param name="message">The message text.</param>
        void Warning(string message);

        /// <summary>
        /// Logs a message with Error severity.
        /// </summary>
        /// <param name="message">The message text.</param>
        void Error(string message);

        /// <summary>
        /// Logs a message with Fatal severity.
        /// </summary>
        /// <param name="message">The message text.</param>
        void Fatal(string message);

        #endregion
    }
}
