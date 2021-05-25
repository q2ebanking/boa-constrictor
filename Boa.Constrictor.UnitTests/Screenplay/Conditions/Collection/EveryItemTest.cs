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
        public void AndEveryItemIsEqualToValue_WithOneItemEqual_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndEveryItem(IsEqualTo.Value(1)).Evaluate(array).Should().BeFalse();
        }

        [Test]
        public void AndEveryItemIsEqualToValue_WithAllItemsEqual_ShouldBeTrue()
        {
            int[] array = { 1, 1, 1 };
            IsACollectionOfType<int>.AndEveryItem(IsEqualTo.Value(1)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void AndEveryItemIsEqualToValue_WithNoItemsEqual_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndEveryItem(IsEqualTo.Value(4)).Evaluate(array).Should().BeFalse();
        }

        [Test]
        public void AndEveryItemIsGreaterThanValue_WithOneItemGreater_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndEveryItem(IsGreaterThan.Value(2)).Evaluate(array).Should().BeFalse();
        }

        [Test]
        public void AndEveryItemIsGreaterThanValue_WithAllItemsGreater_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndEveryItem(IsGreaterThan.Value(0)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void AndEveryItemIsGreaterThanValue_WithNoItemsGreater_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndEveryItem(IsGreaterThan.Value(3)).Evaluate(array).Should().BeFalse();
        }

        #endregion
    }
}
