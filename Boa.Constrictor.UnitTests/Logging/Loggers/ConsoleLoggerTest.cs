using Boa.Constrictor.Logging;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;

namespace Boa.Constrictor.UnitTests.Logging
{
    [TestFixture]
    public class ConsoleLoggerTest
    {
        #region Variables

        private ConsoleLogger Logger;

        #endregion

        #region ConsoleOutput Inner Class

        /// <summary>
        /// https://www.codeproject.com/Articles/501610/Getting-Console-Output-Within-A-Unit-Test
        /// </summary>
        class ConsoleOutput : IDisposable
        {
            private StringWriter NewWriter;
            private readonly TextWriter OldWriter;

            public ConsoleOutput()
            {
                NewWriter = new StringWriter();
                OldWriter = Console.Out;
                Console.SetOut(NewWriter);
            }

            public string GetOutput()
            {
                return NewWriter.ToString();
            }

            public void Dispose()
            {
                Console.SetOut(OldWriter);
                NewWriter.Dispose();
            }
        }

        #endregion

        #region SetUp

        [SetUp]
        public void InitConsoleLogger()
        {
            Logger = new ConsoleLogger();
        }

        #endregion

        #region Tests

        [Test]
        public void Close()
        {
            using var output = new ConsoleOutput();
            Logger.Close();
            output.GetOutput().Trim().Should().Be("");
        }

        [Test]
        public void LogArtifact()
        {
            using var output = new ConsoleOutput();
            const string type = "Screenshot";
            const string path = "path/to/screen.png";
            Logger.LogArtifact(type, path);
            output.GetOutput().Trim().Should().MatchRegex(MessageFormatTest.TimePattern).And.EndWith($"[INFO] {type}: {path}");
        }

        [TestCase("Trace")]
        [TestCase("Debug")]
        [TestCase("Info")]
        [TestCase("Warning")]
        [TestCase("Error")]
        [TestCase("Fatal")]
        public void LogByLevel(string level)
        {
            using var output = new ConsoleOutput();
            const string message = "Message text!";
            Logger.GetType().GetMethod(level).Invoke(Logger, new object[] { message });
            output.GetOutput().Trim().Should().MatchRegex(MessageFormatTest.TimePattern).And.EndWith($"[{level.ToUpper()}] {message}");
        }

        [TestCase("Info")]
        [TestCase("Warning")]
        [TestCase("Error")]
        [TestCase("Fatal")]
        public void LowestSeverityLogged(string level)
        {
            using var output = new ConsoleOutput();
            const string message = "Message text!";
            Logger.LowestSeverity = LogSeverity.Info;
            Logger.GetType().GetMethod(level).Invoke(Logger, new object[] { message });
            output.GetOutput().Trim().Should().MatchRegex(MessageFormatTest.TimePattern).And.EndWith($"[{level.ToUpper()}] {message}");
        }

        [TestCase("Trace")]
        [TestCase("Debug")]
        [TestCase("Info")]
        [TestCase("Warning")]
        [TestCase("Error")]
        public void LowestSeverityBlocked(string level)
        {
            using var output = new ConsoleOutput();
            const string message = "Message text!";
            Logger.LowestSeverity = LogSeverity.Fatal;
            Logger.GetType().GetMethod(level).Invoke(Logger, new object[] { message });
            output.GetOutput().Trim().Should().BeEmpty();
        }

        #endregion
    }
}
