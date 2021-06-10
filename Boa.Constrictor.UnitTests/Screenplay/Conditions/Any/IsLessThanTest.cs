using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    [TestFixture]
    public class IsLessThanTest
    {
        #region Tests

        [Test]
        public void Greater_False()
        {
            IsLessThan.Value(1).Evaluate(2).Should().BeFalse();
        }

        [Test]
        public void Less_True()
        {
            IsLessThan.Value(2).Evaluate(1).Should().BeTrue();
        }

        [Test]
        public void Equal_False()
        {
            IsLessThan.Value(1).Evaluate(1).Should().BeFalse();
        }

        #endregion
    }
}
