using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class SelectedStateTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestElementSelected()
        {
            var element = new Mock<IWebElement>();
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());
            WebDriver.SetupGet(x => x.FindElement(It.IsAny<By>()).Selected).Returns(true);

            Actor.AsksFor(SelectedState.Of(Locator)).Should().BeTrue();
        }

        [Test]
        public void TestElementDoesNotExist()
        {
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement>().AsReadOnly());

            Actor.Invoking(x => x.AsksFor(SelectedState.Of(Locator))).Should().Throw<WaitingException<bool>>();
        }

        [Test]
        public void TestElementNotSelected()
        {
            var element = new Mock<IWebElement>();
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());
            WebDriver.SetupGet(x => x.FindElement(It.IsAny<By>()).Selected).Returns(false);

            Actor.AsksFor(SelectedState.Of(Locator)).Should().BeFalse();
        }

        #endregion
    }
}
