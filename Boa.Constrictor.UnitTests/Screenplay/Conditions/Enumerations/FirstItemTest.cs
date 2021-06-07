using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    [TestFixture]
    public class FirstItemTest
    {
        #region Tests

        [Test]
        public void WhereTheFirstItemIsEqualToValue_WithFirstItemEqual_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereTheFirstItem(IsEqualTo.Value(1)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void WhereTheFirstItemIsEqualToValue_WithFirstItemNotEqual_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereTheFirstItem(IsEqualTo.Value(2)).Evaluate(array).Should().BeFalse();
        }

        [Test]
        public void WhereTheFirstItemIsGreaterThanValue_WithFirstItemGreater_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereTheFirstItem(IsGreaterThan.Value(0)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void WhereTheFirstItemIsGreaterThanValue_WithFirstItemNotGreater_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereTheFirstItem(IsGreaterThan.Value(1)).Evaluate(array).Should().BeFalse();
        }

        #endregion
    }
}
