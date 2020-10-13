using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    [TestFixture]
    public class IsDateAsStringTest
    {
        #region Tests

        [Test]
        public void True_SameFormat()
        {
            IsDateAsString.Value("2019-03-28").Evaluate("2019-03-28").Should().BeTrue();
        }

        [Test]
        public void True_DifferentFormat()
        {
            IsDateAsString.Value("2019-03-28").Evaluate("3/28/2019").Should().BeTrue();
        }

        [Test]
        public void False_SameFormat()
        {
            IsDateAsString.Value("2019-03-28").Evaluate("2019-03-29").Should().BeFalse();
        }

        [Test]
        public void False_DifferentFormat()
        {
            IsDateAsString.Value("2019-03-28").Evaluate("3/29/2019").Should().BeFalse();
        }

        #endregion
    }
}
