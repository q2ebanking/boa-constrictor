using Boa.Constrictor.Logging;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Logging
{
    [TestFixture]
    public class NoOpLoggerTest
    {
        #region Variables

        private NoOpLogger Logger;

        #endregion

        #region SetUp

        [SetUp]
        public void InitConsoleLogger()
        {
            Logger = new NoOpLogger();
        }

        #endregion

        #region Tests

        [Test]
        public void Close()
        {
            Logger.Info("hello");
            Logger.Info("moto");
            Logger.Invoking(y => y.Close()).Should().NotThrow();
        }

        [Test]
        public void LogArtifact()
        {
            Logger.LogArtifact("Screenshot", "path/to/screen.png");
            Logger.Invoking(y => y.Close()).Should().NotThrow();
        }

        [TestCase("Trace")]
        [TestCase("Debug")]
        [TestCase("Info")]
        [TestCase("Warning")]
        [TestCase("Error")]
        [TestCase("Fatal")]
        public void LogByLevel(string level)
        {
            const string message = "Message text!";
            Logger.Invoking(y => y.GetType().GetMethod(level).Invoke(Logger, new object[] { message })).Should().NotThrow();
        }

        #endregion
    }
}
