using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    [TestFixture]
    public class AtLeastOneItemTest
    {
        #region Tests

        [Test]
        public void WhereAtLeastOneItemIsEqualToValue_WithOneItemEqual_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereAtLeastOneItem(IsEqualTo.Value(1)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void WhereAtLeastOneItemIsEqualToValue_WithAllItemsEqual_ShouldBeTrue()
        {
            int[] array = { 1, 1, 1 };
            IsAnEnumerable<int>.WhereAtLeastOneItem(IsEqualTo.Value(1)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void WhereAtLeastOneItemIsEqualToValue_WithNoItemsEqual_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereAtLeastOneItem(IsEqualTo.Value(4)).Evaluate(array).Should().BeFalse();
        }

        [Test]
        public void WhereAtLeastOneItemIsGreaterThanValue_WithOneItemGreater_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereAtLeastOneItem(IsGreaterThan.Value(2)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void WhereAtLeastOneItemIsGreaterThanValue_WithAllItemsGreater_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereAtLeastOneItem(IsGreaterThan.Value(0)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void WhereAtLeastOneItemIsGreaterThanValue_WithNoItemsGreater_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereAtLeastOneItem(IsGreaterThan.Value(3)).Evaluate(array).Should().BeFalse();
        }

        #endregion
    }
}
