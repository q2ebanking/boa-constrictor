using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    [TestFixture]
    public class CountTest
    {
        #region Tests

        [Test]
        public void WhereCountIsEqualToValue_WithHasSizeEqual_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereTheCount(IsEqualTo.Value(3)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void WhereCountIsEqualToValue_WithHasSizeNotEqual_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereTheCount(IsEqualTo.Value(2)).Evaluate(array).Should().BeFalse();
        }

        [Test]
        public void WhereCountIsGreaterThanValue_WithHasSizeGreater_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereTheCount(IsGreaterThan.Value(2)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void WhereCountIsGreaterThanValue_WithHasSizeNotGreater_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereTheCount(IsGreaterThan.Value(3)).Evaluate(array).Should().BeFalse();
        }

        #endregion
    }
}
