using Boa.Constrictor.Dumping;
using Boa.Constrictor.Logging;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Boa.Constrictor.UnitTests.Dumping
{
    [TestFixture]
    public class TestLogReportDumperTest
    {
        #region Variables

        private string AssemblyDir;
        private TestLogReportDumper Dumper;
        private string FilePath;

        #endregion

        #region Setup and Teardown

        [SetUp]
        public void SetUp()
        {
            AssemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Dumper = new TestLogReportDumper("Test Log Report Dumper", AssemblyDir, "TestLogReport", "Boa Test Logs");
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }

        #endregion

        #region Tests

        [Test]
        public void DumpReport()
        {
            // WARNING!
            // This unit test does NOT validate the contents of the generated report.
            // It simply verifies that the report is written to a non-empty file.
            // Reports require visual testing because their contents are likely to change.
            // It would be better to manually inspect report contents than to unit test them.
            // Therefore, when making report changes, it is recommended to set a break point, debug, and manually view the file.

            string sspath = Path.Combine(AssemblyDir, "a.png");
            string rpath = Path.Combine(AssemblyDir, "r.json");

            // Create test 1
            var t1step1 = new StepArtifactData("Step 1");
            t1step1.Messages.Add("Hello");
            t1step1.Messages.Add("Moto");
            t1step1.AddArtifact("Screenshots", sspath);
            t1step1.AddArtifact("Requests", rpath);
            var t1step2 = new StepArtifactData("Step 2");
            t1step2.Messages.Add("Moar");
            t1step2.Messages.Add("MoarMoar");
            t1step2.AddArtifact("Screenshots", sspath);
            var test1 = new TestLogData("Test 1");
            test1.Result = "Passed";
            test1.Steps.Add(t1step1);
            test1.Steps.Add(t1step2);

            // Create test 2
            var t2step1 = new StepArtifactData("Step 1");
            t2step1.Messages.Add("Second Hello");
            t2step1.Messages.Add("Second Moto");
            t2step1.AddArtifact("Screenshots", sspath);
            t2step1.AddArtifact("Requests", rpath);
            var t2step2 = new StepArtifactData("Step 2");
            t2step2.Messages.Add("Enough!");
            t2step2.Messages.Add("Stahp!");
            t2step2.AddArtifact("Screenshots", sspath);
            var test2 = new TestLogData("Test 2");
            test2.Result = "Failed";
            test2.Steps.Add(t2step1);
            test2.Steps.Add(t2step2);

            // Create test 3
            var t3step1 = new StepArtifactData("Step 1");
            t3step1.Messages.Add("A");
            t3step1.Messages.Add("B");
            t3step1.Messages.Add("C");
            t3step1.Messages.Add("D");
            t3step1.AddArtifact("Screenshots", sspath);
            t3step1.AddArtifact("Requests", rpath);
            var test3 = new TestLogData("Test 3");
            test3.Result = "Skipped";
            test3.Steps.Add(t3step1);

            // Create list of logs
            var logs = new List<TestLogData>
            {
                test1,
                test2,
                test3
            };

            // Dump the report
            FilePath = Dumper.Dump(logs, relativePath: AssemblyDir);

            // Read and verify the report
            using var file = new StreamReader(FilePath);
            string report = file.ReadToEnd();
            report.Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        public void DumpEmptyList()
        {
            FilePath = Dumper.Dump(new List<TestLogData>());

            using var file = new StreamReader(FilePath);
            string report = file.ReadToEnd();
            report.Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        public void DumpNull()
        {
            Dumper.Invoking(d => d.Dump(null)).Should().Throw<DumpingException>();
        }

        #endregion
    }
}
