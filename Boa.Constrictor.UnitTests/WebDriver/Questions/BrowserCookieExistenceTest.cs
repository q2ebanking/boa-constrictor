using Boa.Constrictor.WebDriver;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class BrowserCookieExistenceTest : BaseWebQuestionTest
    {
        #region Tests

        [Test]
        public void TestBrowserCookieExists()
        {
            WebDriver.Setup(x => x.Manage().Cookies.GetCookieNamed("bee")).Returns(new Cookie("bee", "hive"));

            Actor.AsksFor(BrowserCookieExistence.Named("bee")).Should().Be(true);
        }

        [Test]
        public void TestBrowserCookieNotExist()
        {
            WebDriver.Setup(x => x.Manage().Cookies.GetCookieNamed("bee")).Returns<Cookie>(null);

            Actor.AsksFor(BrowserCookieExistence.Named("bee")).Should().Be(false);
        }

        #endregion
    }
}
