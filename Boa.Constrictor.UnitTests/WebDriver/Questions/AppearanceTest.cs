using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class AppearanceTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestElementAppears()
        {
            WebDriver.SetupGet(x => x.FindElement(It.IsAny<By>()).Displayed).Returns(true);

            Actor.AsksFor(Appearance.Of(Locator)).Should().BeTrue();
        }

        [Test]
        public void TestElementIsStale()
        {
            WebDriver.SetupGet(x => x.FindElement(It.IsAny<By>()).Displayed).Throws(new StaleElementReferenceException("element is stale"));

            Actor.AsksFor(Appearance.Of(Locator)).Should().BeFalse();
            Logger.Messages.Should().ContainMatch("*element is stale*");
        }

        [Test]
        public void TestElementDoesNotAppear()
        {
            WebDriver.SetupGet(x => x.FindElement(It.IsAny<By>()).Displayed).Throws(new NoSuchElementException());

            Actor.AsksFor(Appearance.Of(Locator)).Should().BeFalse();
        }

        #endregion
    }
}
