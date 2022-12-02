using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Boa.Constrictor.Logging
{
    /// <summary>
    /// Sends one message to multiple loggers.
    /// Any number of loggers may be added.
    /// TeeLogger does not handle log severity:
    /// Registered loggers must handle it on their own.
    /// Loggers must implement ITestLogger.
    /// TeeLogger must override every log method,
    /// just in case a registered logger overrode them.
    /// </summary>
    public class TeeLogger : AbstractLogger
    {
        #region Constructors

        /// <summary>
        /// Basic constructor.
        /// Initializes the collection of inner loggers to be empty.
        /// The lowest severity is "Trace".
        /// </summary>
        public TeeLogger() : base(lowestSeverity: LogSeverity.Trace)
        {
            Loggers = new Dictionary<string, ILogger>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Collection of inner loggers.
        /// </summary>
        private Dictionary<string, ILogger> Loggers { get; set; }

        /// <summary>
        /// The number of inner loggers.
        /// </summary>
        public int Count => Loggers.Count;

        #endregion

        #region Logger Management Methods

        /// <summary>
        /// Adds a new logger to be an inner logger.
        /// Returns a reference to this TeeLogger so calls can be chained.
        /// </summary>
        /// <param name="name">The name of the logger.</param>
        /// <param name="logger">The logger object.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public TeeLogger Add(string name, ILogger logger)
        {
            Loggers[name] = logger;
            return this;
        }

        /// <summary>
        /// Checks if this TeeLogger contains an inner logger by name.
        /// </summary>
        /// <param name="name">The name of the logger.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool Contains(string name)
        {
            return Loggers.ContainsKey(name);
        }

        /// <summary>
        /// Gets an inner logger by name.
        /// </summary>
        /// <param name="name">The name of the logger.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public ILogger Get(string name)
        {
            return Loggers[name];
        }

        /// <summary>
        /// Removes an inner logger by name.
        /// Returns true if the inner logger was found and removed.
        /// </summary>
        /// <param name="name">The name of the logger.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool Remove(string name)
        {
            return Loggers.Remove(name);
        }

        #endregion

        #region Log Methods

        /// <summary>
        /// Closes the logging stream for each inner logger.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public override void Close()
        {
            foreach (ILogger logger in Loggers.Values)
                logger.Close();
        }

        /// <summary>
        /// Logs an artifact for each inner logger.
        /// The artifact must be saved to a file, like a screenshot image or a JSON dump.
        /// </summary>
        /// <param name="type">The name for the type of artifact.</param>
        /// <param name="path">The file path to the artifact.</param>
        public override void LogArtifact(string type, string path)
        {
            foreach (ILogger logger in Loggers.Values)
                logger.LogArtifact(type, path);
        }

        /// <summary>
        /// Logs a basic message to each inner logger.
        /// Lowest log severity is not considered.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <param name="severity">The severity level (defaults to Info).</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        protected override void LogRaw(string message, LogSeverity severity = LogSeverity.Info)
        {
            foreach (ILogger logger in Loggers.Values)
                logger.Log(message, severity);
        }

        /// <summary>
        /// Logs a message with Trace severity to each inner logger.
        /// </summary>
        /// <param name="message">The message text.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public override void Trace(string message)
        {
            foreach (ILogger logger in Loggers.Values)
                if (LogSeverity.Trace >= LowestSeverity)
                    logger.Trace(message);
        }

        /// <summary>
        /// Logs a message with Debug severity to each inner logger.
        /// </summary>
        /// <param name="message">The message text.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public override void Debug(string message)
        {
            foreach (ILogger logger in Loggers.Values)
                if (LogSeverity.Debug >= LowestSeverity)
                    logger.Debug(message);
        }

        /// <summary>
        /// Logs a message with Info severity to each inner logger.
        /// </summary>
        /// <param name="message">The message text.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public override void Info(string message)
        {
            foreach (ILogger logger in Loggers.Values)
                if (LogSeverity.Info >= LowestSeverity)
                    logger.Info(message);
        }

        /// <summary>
        /// Logs a message with Warning severity to each inner logger.
        /// </summary>
        /// <param name="message">The message text.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public override void Warning(string message)
        {
            foreach (ILogger logger in Loggers.Values)
                if (LogSeverity.Warning >= LowestSeverity)
                    logger.Warning(message);
        }

        /// <summary>
        /// Logs a message with Error severity to each inner logger.
        /// </summary>
        /// <param name="message">The message text.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public override void Error(string message)
        {
            foreach (ILogger logger in Loggers.Values)
                if (LogSeverity.Error >= LowestSeverity)
                    logger.Error(message);
        }

        /// <summary>
        /// Logs a message with Fatal severity to each inner logger.
        /// </summary>
        /// <param name="message">The message text.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public override void Fatal(string message)
        {
            foreach (ILogger logger in Loggers.Values)
                if (LogSeverity.Fatal >= LowestSeverity)
                    logger.Fatal(message);
        }

        #endregion
    }
}
