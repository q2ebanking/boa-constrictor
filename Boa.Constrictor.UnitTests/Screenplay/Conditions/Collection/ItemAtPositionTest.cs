using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    [TestFixture]
    public class ItemAtPositionTest
    {
        #region Tests

        [Test]
        public void AndTheItemAtPositionIsEqualToValue_WithItemAtPositionEqual_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndTheItemAtPosition(0, IsEqualTo.Value(1)).Evaluate(array).Should().BeTrue();
        }


        [Test]
        public void AndTheItemAtPositionIsEqualToValue_WithItemAtPositionNotEqual_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndTheItemAtPosition(2, IsEqualTo.Value(2)).Evaluate(array).Should().BeFalse();
        }

        [Test]
        public void AndTheItemAtPositionIsEqualToValue_WithNotExistentItemInPosition_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndTheItemAtPosition(3, IsEqualTo.Value(3)).Evaluate(array).Should().BeFalse();
        }

        [Test]
        public void AndTheItemAtPositionIsGreaterThanValue_WithItemAtPositionGreater_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndTheItemAtPosition(0, IsGreaterThan.Value(0)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void AndTheItemAtPositionIsGreaterThanValue_WithItemAtPositionNotGreater_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndTheItemAtPosition(1, IsGreaterThan.Value(2)).Evaluate(array).Should().BeFalse();
        }

        [Test]
        public void AndTheItemAtPositionIsGreaterThanValue_WithNotExistentItemInPosition_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndTheItemAtPosition(3, IsGreaterThan.Value(2)).Evaluate(array).Should().BeFalse();
        }

        #endregion
    }
}
