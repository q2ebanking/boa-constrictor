using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    [TestFixture]
    public class IsGreaterThanTest
    {
        #region Tests

        [Test]
        public void Greater_True()
        {
            IsGreaterThan.Value(1).Evaluate(2).Should().BeTrue();
        }

        [Test]
        public void Less_False()
        {
            IsGreaterThan.Value(2).Evaluate(1).Should().BeFalse();
        }

        [Test]
        public void Equal_False()
        {
            IsGreaterThan.Value(1).Evaluate(1).Should().BeFalse();
        }

        #endregion
    }
}
