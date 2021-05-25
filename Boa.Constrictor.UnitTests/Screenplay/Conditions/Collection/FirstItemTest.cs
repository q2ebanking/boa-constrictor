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
        public void AndTheFirstItemIsEqualToValue_WithFirstItemEqual_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndTheFirstItem(IsEqualTo.Value(1)).Evaluate(array).Should().BeTrue();
        }


        [Test]
        public void AndTheFirstItemIsEqualToValue_WithFirstItemNotEqual_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndTheFirstItem(IsEqualTo.Value(2)).Evaluate(array).Should().BeFalse();
        }

        [Test]
        public void AndTheFirstItemIsGreaterThanValue_WithFirstItemGreater_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndTheFirstItem(IsGreaterThan.Value(0)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void AndTheFirstItemIsGreaterThanValue_WithFirstItemNotGreater_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndTheFirstItem(IsGreaterThan.Value(1)).Evaluate(array).Should().BeFalse();
        }

        #endregion
    }
}
