using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    [TestFixture]
    public class MatchesTest
    {
        #region Tests

        [Test]
        public void True()
        {
            Matches.Regex(new Regex(@"\d+")).Evaluate("12345").Should().BeTrue();
        }

        [Test]
        public void False()
        {
            Matches.Regex(new Regex(@"\d+")).Evaluate("abcde").Should().BeFalse();
        }

        #endregion
    }
}
