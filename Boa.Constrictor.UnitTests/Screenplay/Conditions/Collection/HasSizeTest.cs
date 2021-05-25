using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    [TestFixture]
    public class HasSizeTest
    {
        #region Tests

        [Test]
        public void AndHasSizeThatIsEqualToValue_WithHasSizeEqual_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndHasSizeThat(IsEqualTo.Value(3)).Evaluate(array).Should().BeTrue();
        }


        [Test]
        public void AndHasSizeThatIsEqualToValue_WithHasSizeNotEqual_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndHasSizeThat(IsEqualTo.Value(2)).Evaluate(array).Should().BeFalse();
        }

        [Test]
        public void AndHasSizeThatIsGreaterThanValue_WithHasSizeGreater_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndHasSizeThat(IsGreaterThan.Value(2)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void AndHasSizeThatIsGreaterThanValue_WithHasSizeNotGreater_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsACollectionOfType<int>.AndHasSizeThat(IsGreaterThan.Value(3)).Evaluate(array).Should().BeFalse();
        }

        #endregion
    }
}
