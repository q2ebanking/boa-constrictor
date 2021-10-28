using Boa.Constrictor.WebDriver;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class BrowserCookieTest : BaseWebQuestionTest
    {
        #region Tests

        [Test]
        public void TestBrowserCookieExists()
        {
            var cookieA = new Cookie("apple", "tree");
            var cookieB = new Cookie("bee", "hive");

            WebDriver.SetupGet(x => x.Manage().Cookies.AllCookies)
                .Returns(new List<Cookie> { cookieA, cookieB }.AsReadOnly());
            WebDriver.Setup(x => x.Manage().Cookies.GetCookieNamed("apple")).Returns(cookieA);
            WebDriver.Setup(x => x.Manage().Cookies.GetCookieNamed("bee")).Returns(cookieB);

            Actor.AsksFor(BrowserCookie.Named("bee")).Value.Should().Be("hive");
        }

        [Test]
        public void TestBrowserCookieNoCookies()
        {
            WebDriver.SetupGet(x => x.Manage().Cookies.AllCookies)
                .Returns(new List<Cookie>().AsReadOnly());

            Actor.Invoking(x => x.AsksFor(BrowserCookie.Named("does not exist")))
                .Should().Throw<BrowserInteractionException>()
                .WithMessage("The browser does not contain a cookie named 'does not exist'");

            Logger.Messages.Should().ContainMatch("*The browser does not contain any cookies");
        }

        [Test]
        public void TestBrowserCookieWrongCookie()
        {
            var cookieA = new Cookie("apple", "tree");
            var cookieB = new Cookie("bee", "hive");

            WebDriver.SetupGet(x => x.Manage().Cookies.AllCookies)
                .Returns(new List<Cookie> { cookieA, cookieB }.AsReadOnly());
            WebDriver.Setup(x => x.Manage().Cookies.GetCookieNamed("apple")).Returns(cookieA);
            WebDriver.Setup(x => x.Manage().Cookies.GetCookieNamed("bee")).Returns(cookieB);

            Actor.Invoking(x => x.AsksFor(BrowserCookie.Named("does not exist")))
                .Should().Throw<BrowserInteractionException>()
                .WithMessage("The browser does not contain a cookie named 'does not exist'");

            Logger.Messages.Should().ContainMatch("*The browser contains the following cookies:");
            Logger.Messages.Should().ContainMatch("*apple: tree");
            Logger.Messages.Should().ContainMatch("*bee: hive");
        }

        #endregion
    }
}
