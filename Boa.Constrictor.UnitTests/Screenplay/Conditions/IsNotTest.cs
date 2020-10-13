using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    [TestFixture]
    public class IsNotTest
    {
        #region Tests

        [Test]
        public void IsNotEqualTo_Int_True()
        {
            IsNotEqualTo.Value(1).Evaluate(2).Should().BeTrue();
        }

        [Test]
        public void IsNotEqualTo_Int_False()
        {
            IsNotEqualTo.Value(1).Evaluate(1).Should().BeFalse();
        }

        [Test]
        public void IsNotEqualTo_String_True()
        {
            IsNotEqualTo.Value("hello").Evaluate("goodbye").Should().BeTrue();
        }

        [Test]
        public void IsNotEqualTo_String_False()
        {
            IsNotEqualTo.Value("hello").Evaluate("hello").Should().BeFalse();
        }

        #endregion
    }
}
