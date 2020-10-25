namespace Boa.Constrictor.Logging
{
    /// <summary>
    /// Does no logging.
    /// It is a much safer alternative than using "null" for logger objects.
    /// </summary>
    public class NoOpLogger : AbstractLogger
    {
        #region Log Method Implementations

        /// <summary>
        /// No-op.
        /// </summary>
        public override void Close() { }

        /// <summary>
        /// No-op.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <param name="severity">The severity level (defaults to Info).</param>
        protected override void LogRaw(string message, LogSeverity severity = LogSeverity.Info) { }

        #endregion
    }
}
