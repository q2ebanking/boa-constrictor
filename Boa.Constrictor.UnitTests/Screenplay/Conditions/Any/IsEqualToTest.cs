using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    [TestFixture]
    public class IsEqualToTest
    {
        #region Tests

        [Test]
        public void Int_True()
        {
            IsEqualTo.Value(1).Evaluate(1).Should().BeTrue();
        }

        [Test]
        public void Int_False()
        {
            IsEqualTo.Value(1).Evaluate(2).Should().BeFalse();
        }

        [Test]
        public void String_True()
        {
            IsEqualTo.Value("hello").Evaluate("hello").Should().BeTrue();
        }

        [Test]
        public void String_False()
        {
            IsEqualTo.Value("hello").Evaluate("goodbye").Should().BeFalse();
        }

        [Test]
        public void String_Null_True()
        {
            IsEqualTo<string>.Value(null).Evaluate(null).Should().BeTrue();
        }

        [Test]
        public void String_Actual_Null_False()
        {
            IsEqualTo.Value("something").Evaluate(null).Should().BeFalse();
        }

        [Test]
        public void String_Expected_Null_False()
        {
            IsEqualTo<string>.Value(null).Evaluate("something").Should().BeFalse();
        }

        [Test]
        public void Bool_True_True()
        {
            IsEqualTo.True().Evaluate(true).Should().BeTrue();
        }

        [Test]
        public void Bool_True_False()
        {
            IsEqualTo.True().Evaluate(false).Should().BeFalse();
        }

        [Test]
        public void Bool_False_True()
        {
            IsEqualTo.False().Evaluate(true).Should().BeFalse();
        }

        [Test]
        public void Bool_False_False()
        {
            IsEqualTo.False().Evaluate(false).Should().BeTrue();
        }

        #endregion
    }
}
