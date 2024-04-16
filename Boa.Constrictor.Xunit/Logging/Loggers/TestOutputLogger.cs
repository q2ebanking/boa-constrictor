using Boa.Constrictor.Screenplay;
using Xunit.Abstractions;

namespace Boa.Constrictor.Xunit
{
    /// <summary>
    /// Prints messages to xUnit's ITestOutputHelper
    /// </summary>
    public class TestOutputLogger : AbstractLogger
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="testOutputHelper">The xUnit logger object</param>
        /// <param name="lowestSeverity">The lowest severity message to log.</param>
        public TestOutputLogger(ITestOutputHelper testOutputHelper, LogSeverity lowestSeverity = LogSeverity.Trace) : base(lowestSeverity)
        {
            TestOutputHelper = testOutputHelper;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The xUnit logger object
        /// </summary>
        public ITestOutputHelper TestOutputHelper { get; }

        #endregion

        #region Log Method Implementations

        /// <summary>
        /// Closes the logging stream
        /// (No-op for ITestOutputHelper)
        /// </summary>
        public override void Close()
        {
        }

        /// <summary>
        /// Logs a basic message to the console after checking the lowest severity
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <param name="severity">The severity level (defaults to info)</param>
        protected override void LogRaw(string message, LogSeverity severity = LogSeverity.Info)
        {
            if (severity >= LowestSeverity)
            {
                TestOutputHelper.WriteLine(MessageFormat.StandardTimestamp(message, severity));
            }
        }

        #endregion
    }
}