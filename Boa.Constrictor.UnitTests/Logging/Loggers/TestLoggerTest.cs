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

        private TestLogger Logger;

        #endregion

        #region Setup and Teardown

        [SetUp]
        public void SetUp()
        {
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Logger = new TestLogger("Unit Test", dir);
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
        public void DumpEmptyTestLog()
        {
            Logger.Close();

            using var file = new StreamReader(Logger.TestLogPath);
            var data = JsonConvert.DeserializeObject<TestLogData>(file.ReadToEnd());

            data.Name.Should().Be("Unit Test");
            data.Steps.Should().BeEmpty();
        }

        [Test]
        public void DumpTestLogWithMessages()
        {
            Logger.LogStep("A");
            Logger.Info("Hello");
            Logger.Warning("Oh no!");
            Logger.Error("ERROR");
            Logger.LogStep("B");
            Logger.Debug("Here's a secret");
            Logger.Close();

            using var file = new StreamReader(Logger.TestLogPath);
            var data = JsonConvert.DeserializeObject<TestLogData>(file.ReadToEnd());

            data.Name.Should().Be("Unit Test");
            data.Steps.Count.Should().Be(2);
            data.Steps[0].Name.Should().Be("A");
            data.Steps[0].Messages.Count.Should().Be(3);
            data.Steps[0].Messages[0].Should().Contain("Hello");
            data.Steps[0].Messages[1].Should().Contain("Oh no!");
            data.Steps[0].Messages[2].Should().Contain("ERROR");
            data.Steps[1].Name.Should().Be("B");
            data.Steps[1].Messages.Count.Should().Be(1);
            data.Steps[1].Messages[0].Should().Contain("Here's a secret");
        }

        #endregion
    }
}
