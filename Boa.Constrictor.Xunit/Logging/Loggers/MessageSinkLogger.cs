namespace Boa.Constrictor.Xunit
{
    using Boa.Constrictor.Screenplay;
    using global::Xunit.Abstractions;
    using global::Xunit.Sdk;

    /// <summary>
    /// Prints messages to xUnit's IMessageSink.
    /// </summary>
    public class MessageSinkLogger : AbstractLogger
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="messageSink">the logger object used by xUnit's extensibility classes.</param>
        /// <param name="lowestSeverity">The lowest severity message to log.</param>
        public MessageSinkLogger(IMessageSink messageSink, LogSeverity lowestSeverity = LogSeverity.Trace)
            :base(lowestSeverity)
        {
            MessageSink = messageSink;
        }

        #endregion

        #region Properties

        /// <summary>
        /// A logger object used by xUnit's extensibility classes.
        /// </summary>
        public IMessageSink MessageSink { get; set; }

        #endregion

        #region Log Method Implementations

        /// <summary>
        /// Closes the logging stream
        /// (No-op for IMessageSink)
        /// </summary>
        public override void Close()
        {
        }

        /// <summary>
        /// Logs a basic message to the console after checking the lowest severity.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <param name="severity">The severity level (defaults to info).</param>
        protected override void LogRaw(string message, LogSeverity severity = LogSeverity.Info)
        {
            if (severity >= LowestSeverity)
            {
                var diagnosticMessage = new DiagnosticMessage(MessageFormat.StandardTimestamp(message, severity));
                MessageSink.OnMessage(diagnosticMessage);
            }
        }

        #endregion
    }
}