using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class EnabledStateTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestElementEnabled()
        {
            var element = new Mock<IWebElement>();
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());
            WebDriver.SetupGet(x => x.FindElement(It.IsAny<By>()).Enabled).Returns(true);

            Actor.AsksFor(EnabledState.Of(Locator)).Should().BeTrue();
        }

        [Test]
        public void TestElementDoesNotExist()
        {
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement>().AsReadOnly());

            Actor.Invoking(x => x.AsksFor(EnabledState.Of(Locator))).Should().Throw<WaitingException<bool>>();
        }

        [Test]
        public void TestElementNotEnabled()
        {
            var element = new Mock<IWebElement>();
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());
            WebDriver.SetupGet(x => x.FindElement(It.IsAny<By>()).Enabled).Returns(false);

            Actor.AsksFor(EnabledState.Of(Locator)).Should().BeFalse();
        }

        #endregion
    }
}
