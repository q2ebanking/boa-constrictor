using Boa.Constrictor.Dumping;
using FluentAssertions;
using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace Boa.Constrictor.UnitTests.Dumping
{
    [TestFixture]
    public class ByteDumperTest
    {
        #region Variables

        private ByteDumper Dumper;
        private string FilePath;

        #endregion

        #region Setup and Teardown

        [SetUp]
        public void SetUp()
        {
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Dumper = new ByteDumper("Test Dumper", dir, "Byte");
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
        public void DumpTextAsBytes()
        {
            const string message = "Here's a test string!";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            FilePath = Dumper.Dump(data, ".txt");

            string text = File.ReadAllText(FilePath);
            text.Should().Be(message);
        }

        [Test]
        public void DumpTextAsBytesWithoutExtension()
        {
            const string message = "Here's a test string!";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            FilePath = Dumper.Dump(data);
            FilePath.Should().NotEndWith(".txt");

            string text = File.ReadAllText(FilePath);
            text.Should().Be(message);
        }

        [Test]
        public void DumpNull()
        {
            Dumper.Invoking(d => d.Dump(null, "")).Should().Throw<DumpingException>();
        }

        #endregion
    }
}
