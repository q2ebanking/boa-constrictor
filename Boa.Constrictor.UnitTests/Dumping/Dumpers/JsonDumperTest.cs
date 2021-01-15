using Boa.Constrictor.Dumping;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace Boa.Constrictor.UnitTests.Dumping
{
    [TestFixture]
    public class JsonDumperTest
    {
        #region Data Classes

        private class Data
        {
            public int Number { get; set; }
            public string Text { get; set; }
            public bool Binary { get; set; }
        }

        #endregion

        #region Variables

        private JsonDumper Dumper;
        private string FilePath;

        #endregion

        #region Setup and Teardown

        [SetUp]
        public void SetUp()
        {
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Dumper = new JsonDumper("Test Dumper", dir, "JSON");
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
        public void DumpObject()
        {
            Data data = new Data
            {
                Number = 1,
                Text = "Hello World",
                Binary = true
            };

            FilePath = Dumper.Dump(data);

            using var file = new StreamReader(FilePath);
            var dataFromFile = JsonConvert.DeserializeObject<Data>(file.ReadToEnd());
            dataFromFile.Number.Should().Be(1);
            dataFromFile.Text.Should().Be("Hello World");
            dataFromFile.Binary.Should().BeTrue();
        }

        [Test]
        public void DumpNull()
        {
            FilePath = Dumper.Dump(null);

            using var file = new StreamReader(FilePath);
            object dataFromFile = JsonConvert.DeserializeObject<Data>(file.ReadToEnd());
            dataFromFile.Should().BeNull();
        }

        #endregion
    }
}
