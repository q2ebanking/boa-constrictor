using Boa.Constrictor.Logging;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Logging
{
    [TestFixture]
    public class TeeLoggerTest
    {
        public class Management
        {
            #region Variables

            private TeeLogger Logger;

            #endregion

            #region SetUp

            [SetUp]
            public void InitTeeLogger()
            {
                Logger = new TeeLogger();
            }

            #endregion

            #region Tests

            [Test]
            public void Init()
            {
                Logger.Count.Should().Be(0);
            }

            [Test]
            public void AddOneLogger()
            {
                ListLogger a = new ListLogger();
                Logger.Add("a", a);

                Logger.Count.Should().Be(1);
                Logger.Contains("a").Should().BeTrue();
                Logger.Get("a").Should().BeSameAs(a);
            }

            [Test]
            public void AddTwoLoggers()
            {
                ListLogger a = new ListLogger();
                ListLogger b = new ListLogger();
                Logger.Add("a", a);
                Logger.Add("b", b);

                Logger.Count.Should().Be(2);
                Logger.Contains("a").Should().BeTrue();
                Logger.Contains("b").Should().BeTrue();
                Logger.Get("a").Should().BeSameAs(a);
                Logger.Get("b").Should().BeSameAs(b);
            }

            [Test]
            public void RemoveLogger()
            {
                ListLogger a = new ListLogger();
                ListLogger b = new ListLogger();
                Logger.Add("a", a);
                Logger.Add("b", b);
                bool removed = Logger.Remove("a");

                removed.Should().BeTrue();
                Logger.Count.Should().Be(1);
                Logger.Contains("a").Should().BeFalse();
                Logger.Contains("b").Should().BeTrue();
            }

            [Test]
            public void AttemptToRemoveNonexistentLogger()
            {
                ListLogger a = new ListLogger();
                ListLogger b = new ListLogger();
                Logger.Add("a", a);
                Logger.Add("b", b);
                bool removed = Logger.Remove("c");

                removed.Should().BeFalse();
                Logger.Count.Should().Be(2);
                Logger.Contains("a").Should().BeTrue();
                Logger.Contains("b").Should().BeTrue();
            }

            #endregion
        }

        public class Logging
        {

            #region Variables

            private const int LoggerCount = 3;
            private TeeLogger Logger;

            #endregion

            #region SetUp

            [SetUp]
            public void InitConsoleLogger()
            {
                Logger = new TeeLogger();

                for (int i = 1; i <= LoggerCount; i++)
                    Logger.Add(i.ToString(), new ListLogger());
            }

            #endregion

            #region Log Tests

            [Test]
            public void Close()
            {
                Logger.Info("hello");
                Logger.Info("moto");
                Logger.Close();

                for (int i = 1; i <= LoggerCount; i++)
                {
                    ListLogger lister = (ListLogger)Logger.Get(i.ToString());
                    lister.Messages.Count.Should().Be(2);
                }
            }

            [Test]
            public void LogArtifact()
            {
                const string type = "Screenshot";
                const string path = "path/to/screen.png";
                Logger.LogArtifact(type, path);

                for (int i = 1; i <= LoggerCount; i++)
                {
                    ListLogger lister = (ListLogger)Logger.Get(i.ToString());
                    lister.Messages.Count.Should().Be(1);
                    lister.Messages[0].Should().MatchRegex(MessageFormatTest.TimePattern).And.EndWith($"[INFO] {type}: {path}");
                }
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

                for (int i = 1; i <= LoggerCount; i++)
                {
                    ListLogger lister = (ListLogger)Logger.Get(i.ToString());
                    lister.Messages[0].Should().MatchRegex(MessageFormatTest.TimePattern).And.EndWith($"[{level.ToUpper()}] {message}");
                }
            }

            #endregion
        }
    }
}
