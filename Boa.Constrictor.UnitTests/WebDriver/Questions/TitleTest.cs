using Boa.Constrictor.WebDriver;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class TitleTest : BaseWebQuestionTest
    {
        #region Tests

        [Test]
        public void TestGetTitle()
        {
            WebDriver.SetupGet(x => x.Title).Returns("Google Search Results");
            Actor.AsksFor(Title.OfPage()).Should().Be("Google Search Results");
        }

        #endregion
    }
}
