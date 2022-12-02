namespace Boa.Constrictor.Logging
{
    /// <summary>
    /// An abstract implementation of ILogger.
    /// The log-by-level methods can all use the basic Log method directly.
    /// However, the main Log methods must be implemented by a concrete subclass.
    /// </summary>
    public abstract class AbstractLogger : ILogger
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="lowestSeverity">The lowest severity message to log.</param>
        public AbstractLogger(LogSeverity lowestSeverity = LogSeverity.Trace)
        {
            LowestSeverity = lowestSeverity;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The lowest severity message to log.
        /// </summary>
        public LogSeverity LowestSeverity { get; set; }

        #endregion

        #region Abstract Log Methods

        /// <summary>
        /// Closes the logging stream.
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// Logs a basic message after checking the lowest severity.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <param name="severity">The severity level (defaults to Info).</param>
        protected abstract void LogRaw(string message, LogSeverity severity = LogSeverity.Info);

        #endregion

        #region Log Methods

        /// <summary>
        /// Logs a basic message if the severity is greater than the lowest severity.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <param name="severity">The severity level (defaults to Info).</param>
        public virtual void Log(string message, LogSeverity severity = LogSeverity.Info)
        {
            if (severity >= LowestSeverity)
                LogRaw(message, severity: severity);
        }

        /// <summary>
        /// Logs an artifact.
        /// The artifact must be saved to a file, like a screenshot image or a JSON dump.
        /// </summary>
        /// <param name="type">The name for the type of artifact.</param>
        /// <param name="path">The file path to the artifact.</param>
        public virtual void LogArtifact(string type, string path) => Info($"{type}: {path}");

        #endregion

        #region Log-by-Level Methods

        /// <summary>
        /// Logs a message with Trace severity.
        /// </summary>
        /// <param name="message">The message text.</param>
        public virtual void Trace(string message) => Log(message, LogSeverity.Trace);

        /// <summary>
        /// Logs a message with Debug severity.
        /// </summary>
        /// <param name="message">The message text.</param>
        public virtual void Debug(string message) => Log(message, LogSeverity.Debug);

        /// <summary>
        /// Logs a message with Info severity.
        /// </summary>
        /// <param name="message">The message text.</param>
        public virtual void Info(string message) => Log(message, LogSeverity.Info);

        /// <summary>
        /// Logs a message with Warning severity.
        /// </summary>
        /// <param name="message">The message text.</param>
        public virtual void Warning(string message) => Log(message, LogSeverity.Warning);

        /// <summary>
        /// Logs a message with Error severity.
        /// </summary>
        /// <param name="message">The message text.</param>
        public virtual void Error(string message) => Log(message, LogSeverity.Error);

        /// <summary>
        /// Logs a message with Fatal severity.
        /// </summary>
        /// <param name="message">The message text.</param>
        public virtual void Fatal(string message) => Log(message, LogSeverity.Fatal);

        #endregion
    }
}
