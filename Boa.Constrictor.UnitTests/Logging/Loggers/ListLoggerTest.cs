using Boa.Constrictor.Logging;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Logging
{
    [TestFixture]
    public class ListLoggerTest
    {
        #region Variables

        private ListLogger Logger;

        #endregion

        #region SetUp

        [SetUp]
        public void InitListLogger()
        {
            Logger = new ListLogger();
        }

        #endregion

        #region Tests

        [Test]
        public void Init()
        {
            Logger.Messages.Count.Should().Be(0);
        }

        [Test]
        public void Close()
        {
            Logger.Info("hello");
            Logger.Info("moto");
            Logger.Close();
            Logger.Messages.Count.Should().Be(2);
        }

        [Test]
        public void LogArtifact()
        {
            const string type = "Screenshot";
            const string path = "path/to/screen.png";
            Logger.LogArtifact(type, path);
            Logger.Messages.Count.Should().Be(1);
            Logger.Messages[0].Should().MatchRegex(MessageFormatTest.TimePattern).And.EndWith($"[INFO] {type}: {path}");
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
            Logger.GetType().GetMethod(level).Invoke(Logger, new object[] { message });
            Logger.Messages[0].Should().MatchRegex(MessageFormatTest.TimePattern).And.EndWith($"[{level.ToUpper()}] {message}");
        }

        [TestCase("Info")]
        [TestCase("Warning")]
        [TestCase("Error")]
        [TestCase("Fatal")]
        public void LowestSeverityLogged(string level)
        {
            const string message = "Message text!";
            Logger.LowestSeverity = LogSeverity.Info;
            Logger.GetType().GetMethod(level).Invoke(Logger, new object[] { message });
            Logger.Messages[0].Should().MatchRegex(MessageFormatTest.TimePattern).And.EndWith($"[{level.ToUpper()}] {message}");
        }

        [TestCase("Trace")]
        [TestCase("Debug")]
        [TestCase("Info")]
        [TestCase("Warning")]
        [TestCase("Error")]
        public void LowestSeverityBlocked(string level)
        {
            const string message = "Message text!";
            Logger.LowestSeverity = LogSeverity.Fatal;
            Logger.GetType().GetMethod(level).Invoke(Logger, new object[] { message });
            Logger.Messages.Should().BeEmpty();
        }

        #endregion
    }
}
