using Boa.Constrictor.Logging;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace Boa.Constrictor.UnitTests.Logging
{
    [TestFixture]
    public class TestLoggerTest
    {
        #region Variables

        private string TestLogDir;
        private TestLogger Logger;

        #endregion

        #region Setup and Teardown

        [SetUp]
        public void SetUp()
        {
            TestLogDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Logger = new TestLogger("Unit Test", TestLogDir);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(Logger.TestLogPath))
                File.Delete(Logger.TestLogPath);
        }

        #endregion

        #region Tests

        [Test]
        public void Init()
        {
            Logger.Data.Name.Should().Be("Unit Test");
            Logger.Data.Result.Should().BeNull();
            Logger.Data.Steps.Should().BeEmpty();
            Logger.TestLogDir.Should().Be(TestLogDir);
            Logger.TestLogPath.Should().BeNull();
            Logger.CurrentStep.Should().BeNull();
        }

        public void LogResult()
        {
            Logger.LogResult("Pass");
            Logger.Data.Result.Should().Be("Pass");
        }

        [Test]
        public void LogStep()
        {
            Logger.LogStep("One");
            Logger.Data.Steps.Count.Should().Be(1);
            Logger.Data.Steps[0].Should().Be(Logger.CurrentStep);
            Logger.CurrentStep.Name.Should().Be("One");
            Logger.CurrentStep.Messages.Should().BeEmpty();
            Logger.CurrentStep.Artifacts.Should().BeEmpty();
        }

        [Test]
        public void LogWithoutStep()
        {
            Logger.Invoking(x => x.Info("message"))
                .Should().Throw<LoggingException>()
                .WithMessage("TestLogger does not have its first step");
        }

        [Test]
        public void LogArtifact()
        {
            const string type = "Screenshot";
            const string path = "path/to/screen.png";

            Logger.LogStep("first");
            Logger.LogArtifact(type, path);
            Logger.Data.Steps[0].Messages.Count.Should().Be(1);
            Logger.Data.Steps[0].Messages[0].Should().EndWith($"{type}: {path}");
            Logger.Data.Steps[0].Artifacts.Count.Should().Be(1);
            Logger.Data.Steps[0].Artifacts[type].Count.Should().Be(1);
            Logger.Data.Steps[0].Artifacts[type][0].Should().Be(path);
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

            Logger.LogStep("first");
            Logger.GetType().GetMethod(level).Invoke(Logger, new object[] { message });
            Logger.Data.Steps[0].Messages.Count.Should().Be(1);
            Logger.Data.Steps[0].Messages[0].Should().MatchRegex(MessageFormatTest.TimePattern).And.EndWith($"[{level.ToUpper()}] {message}");
        }

        [TestCase("Info")]
        [TestCase("Warning")]
        [TestCase("Error")]
        [TestCase("Fatal")]
        public void LowestSeverityLogged(string level)
        {
            const string message = "Message text!";

            Logger.LowestSeverity = LogSeverity.Info;
            Logger.LogStep("first");
            Logger.GetType().GetMethod(level).Invoke(Logger, new object[] { message });
            Logger.Data.Steps[0].Messages.Count.Should().Be(1);
            Logger.Data.Steps[0].Messages[0].Should().MatchRegex(MessageFormatTest.TimePattern).And.EndWith($"[{level.ToUpper()}] {message}");
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
            Logger.LogStep("first");
            Logger.GetType().GetMethod(level).Invoke(Logger, new object[] { message });
            Logger.Data.Steps[0].Messages.Should().BeEmpty();
        }

        [Test]
        public void DumpEmptyTestLog()
        {
            Logger.Close();

            using var file = new StreamReader(Logger.TestLogPath);
            var data = JsonConvert.DeserializeObject<TestLogData>(file.ReadToEnd());

            data.Name.Should().Be("Unit Test");
            data.Steps.Should().BeEmpty();
        }

        [Test]
        public void DumpFullLog()
        {
            Logger.LogStep("A");
            Logger.LogArtifact(ArtifactTypes.Screenshots, "a1.png");
            Logger.LogArtifact(ArtifactTypes.Screenshots, "a2.png");
            Logger.LogArtifact(ArtifactTypes.Downloads, "downA.pdf");
            Logger.Info("Hello");
            Logger.Warning("Oh no!");
            Logger.Error("ERROR");
            Logger.LogStep("B");
            Logger.LogArtifact(ArtifactTypes.Screenshots, "b.png");
            Logger.LogArtifact(ArtifactTypes.Downloads, "downB1.pdf");
            Logger.LogArtifact(ArtifactTypes.Downloads, "downB2.pdf");
            Logger.Debug("Here's a secret");
            Logger.LogResult("Fail");
            Logger.Close();

            using var file = new StreamReader(Logger.TestLogPath);
            var data = JsonConvert.DeserializeObject<TestLogData>(file.ReadToEnd());

            data.Name.Should().Be("Unit Test");
            data.Result.Should().Be("Fail");
            data.Steps.Count.Should().Be(2);

            data.Steps[0].Name.Should().Be("A");
            data.Steps[0].Messages.Count.Should().Be(6);
            data.Steps[0].Messages[0].Should().EndWith("Screenshots: a1.png");
            data.Steps[0].Messages[1].Should().EndWith("Screenshots: a2.png");
            data.Steps[0].Messages[2].Should().EndWith("Downloads: downA.pdf");
            data.Steps[0].Messages[3].Should().Contain("Hello");
            data.Steps[0].Messages[4].Should().Contain("Oh no!");
            data.Steps[0].Messages[5].Should().Contain("ERROR");
            data.Steps[0].Artifacts.Count.Should().Be(2);
            data.Steps[0].Artifacts[ArtifactTypes.Screenshots].Count.Should().Be(2);
            data.Steps[0].Artifacts[ArtifactTypes.Screenshots][0].Should().Be("a1.png");
            data.Steps[0].Artifacts[ArtifactTypes.Screenshots][1].Should().Be("a2.png");
            data.Steps[0].Artifacts[ArtifactTypes.Downloads].Count.Should().Be(1);
            data.Steps[0].Artifacts[ArtifactTypes.Downloads][0].Should().Be("downA.pdf");

            data.Steps[1].Name.Should().Be("B");
            data.Steps[1].Messages.Count.Should().Be(4);
            data.Steps[1].Messages[0].Should().EndWith("Screenshots: b.png");
            data.Steps[1].Messages[1].Should().EndWith("Downloads: downB1.pdf");
            data.Steps[1].Messages[2].Should().EndWith("Downloads: downB2.pdf");
            data.Steps[1].Messages[3].Should().Contain("Here's a secret");
            data.Steps[1].Artifacts.Count.Should().Be(2);
            data.Steps[1].Artifacts[ArtifactTypes.Screenshots].Count.Should().Be(1);
            data.Steps[1].Artifacts[ArtifactTypes.Screenshots][0].Should().Be("b.png");
            data.Steps[1].Artifacts[ArtifactTypes.Downloads].Count.Should().Be(2);
            data.Steps[1].Artifacts[ArtifactTypes.Downloads][0].Should().Be("downB1.pdf");
            data.Steps[1].Artifacts[ArtifactTypes.Downloads][1].Should().Be("downB2.pdf");
        }

        #endregion
    }
}
