using Boa.Constrictor.WebDriver;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class CurrentUrlTest : BaseWebQuestionTest
    {
        #region Tests

        [Test]
        public void TestGetUrl()
        {
            WebDriver.SetupGet(x => x.Url).Returns("google.com");
            Actor.AsksFor(CurrentUrl.FromBrowser()).Should().Be("google.com");
        }

        #endregion
    }
}
