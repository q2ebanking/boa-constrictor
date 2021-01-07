using Boa.Constrictor.Dumping;

namespace Boa.Constrictor.Logging
{
    /// <summary>
    /// Logs messages to a JSON log file for one test case.
    /// Breaks test case logging into steps.
    /// Uses TestCaseData and StepArtifactData for JSON serialization.
    /// </summary>
    public class TestLogger : AbstractLogger
    {
        #region Properties

        /// <summary>
        /// The test log directory where to dump the JSON file upon closing the logger.
        /// </summary>
        public string TestLogDir { get; private set; }

        /// <summary>
        /// The test log path where the JSON file is dumped.
        /// This value will be null until the file is dumped upon closing the logger.
        /// </summary>
        public string TestLogPath { get; private set; }

        /// <summary>
        /// The test case data being logged.
        /// </summary>
        public TestLogData Data { get; private set; }

        /// <summary>
        /// The current step data object.
        /// </summary>
        public StepArtifactData CurrentStep { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="testName">The test case name.</param>
        /// <param name="testLogDir">The test log directory where to dump the JSON file upon closing the logger.</param>
        /// <param name="lowestSeverity">The lowest severity message to log.</param>
        public TestLogger(string testName, string testLogDir, LogSeverity lowestSeverity = LogSeverity.Trace) :
            base(lowestSeverity: lowestSeverity)
        {
            TestLogDir = testLogDir;
            TestLogPath = null;
            Data = new TestLogData(testName);
            CurrentStep = null;
        }

        #endregion

        #region Overridden Log Methods

        /// <summary>
        /// Closes the logger and writes the test log as a JSON file.
        /// </summary>
        public override void Close()
        {
            var dumper = new JsonDumper("Test Log Dumper", TestLogDir, "TestLog");
            TestLogPath = dumper.Dump(Data);
        }

        /// <summary>
        /// Logs an artifact.
        /// The artifact must be saved to a file, like a screenshot image or a JSON dump.
        /// </summary>
        /// <param name="type">The name for the type of artifact.</param>
        /// <param name="path">The file path to the artifact.</param>
        public override void LogArtifact(string type, string path)
        {
            base.LogArtifact(type, path);
            CurrentStep.AddArtifact(type, path);
        }

        /// <summary>
        /// Logs a raw message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="severity">The log severity.</param>
        protected override void LogRaw(string message, LogSeverity severity = LogSeverity.Info)
        {
            if (CurrentStep == null)
                throw new LoggingException("TestLogger does not have its first step");

            CurrentStep.Messages.Add(MessageFormat.StandardTimestamp(message, severity));
        }

        #endregion

        #region New Log Methods

        /// <summary>
        /// Logs the test result.
        /// </summary>
        /// <param name="result">The test result.</param>
        public void LogResult(string result)
        {
            Data.Result = result;
        }

        /// <summary>
        /// Logs a new step.
        /// Internally adds new step data.
        /// All messages and artifacts will be logged under this new step.
        /// </summary>
        /// <param name="name">The test step name.</param>
        public void LogStep(string name)
        {
            CurrentStep = new StepArtifactData(name);
            Data.Steps.Add(CurrentStep);
        }

        #endregion
    }
}
