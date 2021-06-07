using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    [TestFixture]
    public class EveryItemTest
    {
        #region Tests

        [Test]
        public void WhereEveryItemIsEqualToValue_WithOneItemEqual_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereEveryItem(IsEqualTo.Value(1)).Evaluate(array).Should().BeFalse();
        }

        [Test]
        public void WhereEveryItemIsEqualToValue_WithAllItemsEqual_ShouldBeTrue()
        {
            int[] array = { 1, 1, 1 };
            IsAnEnumerable<int>.WhereEveryItem(IsEqualTo.Value(1)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void WhereEveryItemIsEqualToValue_WithNoItemsEqual_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereEveryItem(IsEqualTo.Value(4)).Evaluate(array).Should().BeFalse();
        }

        [Test]
        public void WhereEveryItemIsGreaterThanValue_WithOneItemGreater_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereEveryItem(IsGreaterThan.Value(2)).Evaluate(array).Should().BeFalse();
        }

        [Test]
        public void WhereEveryItemIsGreaterThanValue_WithAllItemsGreater_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereEveryItem(IsGreaterThan.Value(0)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void WhereEveryItemIsGreaterThanValue_WithNoItemsGreater_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereEveryItem(IsGreaterThan.Value(3)).Evaluate(array).Should().BeFalse();
        }

        #endregion
    }
}
