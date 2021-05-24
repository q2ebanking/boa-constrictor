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
        public void AndAtLeastOneItemIsEqualToValue_WithOneItemEqual_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndAtLeastOneItem(IsEqualTo.Value(1)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void AndAtLeastOneItemIsEqualToValue_WithAllItemsEqual_ShouldBeTrue()
        {
            int[] array = { 1, 1, 1 };
            IsACollectionOfType<int>.AndAtLeastOneItem(IsEqualTo.Value(1)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void AndAtLeastOneItemIsEqualToValue_WithNoItemsEqual_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndAtLeastOneItem(IsEqualTo.Value(4)).Evaluate(array).Should().BeFalse();
        }


        [Test]
        public void AndAtLeastOneItemIsGreaterThanValue_WithOneItemGreater_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndAtLeastOneItem(IsGreaterThan.Value(2)).Evaluate(array).Should().BeTrue();
        }

        #endregion
    }
}
