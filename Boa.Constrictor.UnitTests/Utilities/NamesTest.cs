using Boa.Constrictor.Utilities;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Utilities
{
    [TestFixture]
    public class NamesTest
    {
        #region Tests

        [Test]
        public void ConcatUniqueName_NoSuffix()
        {
            Names.ConcatUniqueName("name").Should().MatchRegex(@"^name_\d+(_[A-Za-z0-9]+)?$");
        }

        [Test]
        public void ConcatUniqueName_NameWithWhitespace()
        {
            Names.ConcatUniqueName("   name  ").Should().MatchRegex(@"^name_\d+(_[A-Za-z0-9]+)?$");
        }

        [Test]
        public void ConcatUniqueName_Suffix()
        {
            Names.ConcatUniqueName("name", "7").Should().MatchRegex(@"^name_\d+_7(_[A-Za-z0-9]+)?$");
        }

        [Test]
        public void ConcatUniqueName_WhitespaceSuffix()
        {
            Names.ConcatUniqueName("name", "   ").Should().MatchRegex(@"^name_\d+(_[A-Za-z0-9]+)?$");
        }

        #endregion
    }
}
