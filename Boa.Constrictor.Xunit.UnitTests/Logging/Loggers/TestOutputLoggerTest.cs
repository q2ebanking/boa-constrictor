using Boa.Constrictor.Screenplay;
using FluentAssertions;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Boa.Constrictor.Xunit.UnitTests
{
    public class TestOutputLoggerTest
    {
        #region Variables

        const string TimePattern = @"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}";

        public TestOutputHelper OutputHelper;

        public TestOutputLogger Logger;

        #endregion

        #region SetUp

        public TestOutputLoggerTest()
        {
            // Using a concrete TestOutputHelper allows us to access the output
            OutputHelper = new TestOutputHelper();
            Logger = new TestOutputLogger(OutputHelper);

            InitializeOutputHelper();
        }

        /// <summary>
        /// Initializes TestOutputHelper and all of it's dependencies
        /// </summary>
        private void InitializeOutputHelper()
        {

            var messageBus = new Mock<IMessageBus>();
            var test = new Mock<ITest>();
            var testCase = new Mock<ITestCase>();
            var testMethod = new Mock<ITestMethod>();
            var testClass = new Mock<ITestClass>();
            var testCollection = new Mock<ITestCollection>();
            var testAssembly = new Mock<ITestAssembly>();

            testCollection.Setup(x => x.TestAssembly).Returns(testAssembly.Object);

            testClass.Setup(x => x.TestCollection).Returns(testCollection.Object);

            testMethod.Setup(x => x.TestClass).Returns(testClass.Object);

            testCase.Setup(x => x.DisplayName).Returns("test");
            testCase.Setup(x => x.TestMethod).Returns(testMethod.Object);

            test.Setup(x => x.TestCase).Returns(testCase.Object);

            messageBus.Setup(x => x.QueueMessage(It.IsAny<IMessageSinkMessage>())).Returns(true);

            OutputHelper.Initialize(messageBus.Object, test.Object);
        }

        #endregion

        #region Tests

        [Fact]
        public void Close()
        {
            Logger.Info("hello");
            Logger.Info("moto");
            Logger.Invoking(y => y.Close()).Should().NotThrow();
        }


        [Fact]
        public void LogArtifact()
        {
            const string type = "Screenshot";
            const string path = "path/to/screen.png";

            Logger.LogArtifact(type, path);

            OutputHelper.Output.Trim().Should().MatchRegex(TimePattern).And.EndWith($"[INFO] {type}: {path}");
        }

        [Theory]
        [InlineData("Trace")]
        [InlineData("Debug")]
        [InlineData("Info")]
        [InlineData("Warning")]
        [InlineData("Error")]
        [InlineData("Fatal")]
        public void LogByLevel(string level)
        {
            const string message = "Message text!";

            Logger.GetType().GetMethod(level).Invoke(Logger, new object[] { message });

            OutputHelper.Output.Trim().Should().MatchRegex(TimePattern).And.EndWith($"[{level.ToUpper()}] {message}");
        }

        [Theory]
        [InlineData("Info")]
        [InlineData("Warning")]
        [InlineData("Error")]
        [InlineData("Fatal")]
        public void LowestSeverityLogged(string level)
        {
            const string message = "Message text!";
            Logger.LowestSeverity = LogSeverity.Info;

            Logger.GetType().GetMethod(level).Invoke(Logger, new object[] { message });

            OutputHelper.Output.Trim().Should().MatchRegex(TimePattern).And.EndWith($"[{level.ToUpper()}] {message}");
        }

        [Theory]
        [InlineData("Trace")]
        [InlineData("Debug")]
        [InlineData("Info")]
        [InlineData("Warning")]
        [InlineData("Error")]
        public void LowestSeverityBlocked(string level)
        {
            const string message = "Message text!";
            Logger.LowestSeverity = LogSeverity.Fatal;

            Logger.GetType().GetMethod(level).Invoke(Logger, new object[] { message });

            OutputHelper.Output.Trim().Should().BeEmpty();
        }

        #endregion
    }
}