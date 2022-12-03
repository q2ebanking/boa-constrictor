using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.Screenplay.UnitTests
{
    [TestFixture]
    public class IsNullOrWhitespaceTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\t")]

        public void True(string actual)
        {
            IsNullOrWhitespace.Value().Evaluate(actual).Should().BeTrue();
        }

        [TestCase("actualvalue")]
        public void False(string actual)
        {
            IsNullOrWhitespace.Value().Evaluate(actual).Should().BeFalse();
        }
    }
}