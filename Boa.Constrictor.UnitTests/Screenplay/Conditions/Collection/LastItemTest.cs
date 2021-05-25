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
        public void AndTheLastItemIsEqualToValue_WithLastItemEqual_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndTheLastItem(IsEqualTo.Value(3)).Evaluate(array).Should().BeTrue();
        }


        [Test]
        public void AndTheLastItemIsEqualToValue_WithLastItemNotEqual_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndTheLastItem(IsEqualTo.Value(2)).Evaluate(array).Should().BeFalse();
        }

        [Test]
        public void AndTheLastItemIsGreaterThanValue_WithLastItemGreater_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndTheLastItem(IsGreaterThan.Value(2)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void AndTheLastItemIsGreaterThanValue_WithLastItemNotGreater_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndTheLastItem(IsGreaterThan.Value(4)).Evaluate(array).Should().BeFalse();
        }

        #endregion
    }
}
