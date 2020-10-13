using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    [TestFixture]
    public class ContainsSubstringTest
    {
        #region Tests

        [Test]
        public void True()
        {
            ContainsSubstring.Text("World").Evaluate("Hello World!").Should().BeTrue();
        }

        [Test]
        public void False()
        {
            ContainsSubstring.Text("Goodbye").Evaluate("Hello World!").Should().BeFalse();
        }

        #endregion
    }
}
