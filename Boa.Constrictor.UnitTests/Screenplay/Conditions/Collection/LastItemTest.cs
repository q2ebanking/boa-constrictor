using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    [TestFixture]
    public class LastItemTest
    {
        #region Tests

        [Test]
        public void WhereTheLastItemIsEqualToValue_WithLastItemEqual_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereTheLastItem(IsEqualTo.Value(3)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void WhereTheLastItemIsEqualToValue_WithLastItemNotEqual_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereTheLastItem(IsEqualTo.Value(2)).Evaluate(array).Should().BeFalse();
        }

        [Test]
        public void WhereTheLastItemIsGreaterThanValue_WithLastItemGreater_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereTheLastItem(IsGreaterThan.Value(2)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void WhereTheLastItemIsGreaterThanValue_WithLastItemNotGreater_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereTheLastItem(IsGreaterThan.Value(4)).Evaluate(array).Should().BeFalse();
        }

        #endregion
    }
}
