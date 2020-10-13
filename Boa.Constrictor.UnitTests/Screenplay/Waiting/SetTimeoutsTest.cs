using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    [TestFixture]
    public class SetTimeoutsTest
    {
        #region Tests

        [Test]
        public void TestSetTimeoutsToDefaultValues()
        {
            SetTimeouts ability = SetTimeouts.ToDefaultValues();
            ability.StandardSeconds.Should().Be(SetTimeouts.DefaultStandardTimeout);
            ability.ExtraSeconds.Should().Be(SetTimeouts.DefaultExtraTimeout);
        }

        [Test]
        public void TestSetTimeoutsToExplicitValues()
        {
            SetTimeouts ability = SetTimeouts.To(60, 10);
            ability.StandardSeconds.Should().Be(60);
            ability.ExtraSeconds.Should().Be(10);
        }

        [TestCase(60, 0, 60)]
        [TestCase(60, 10, 70)]
        public void CalculateTimeoutWithoutOverride(int standard, int extra, int expected)
        {
            SetTimeouts.To(standard, extra).CalculateTimeout().Should().Be(expected);
        }

        [TestCase(60, 0, 100, 100)]
        [TestCase(60, 10, 100, 110)]
        public void CalculateTimeoutWithOverride(int standard, int extra, int overridden, int expected)
        {
            SetTimeouts.To(standard, extra).CalculateTimeout(overridden).Should().Be(expected);
        }

        #endregion
    }
}
