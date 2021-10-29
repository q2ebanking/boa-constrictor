using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class TagNameTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestTagExists()
        {
            var element = new Mock<IWebElement>();
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());
            WebDriver.SetupGet(x => x.FindElement(It.IsAny<By>()).TagName).Returns("body");

            Actor.AsksFor(TagName.Of(Locator)).Should().Be("body");
        }

        [Test]
        public void TestElementDoesNotExist()
        {
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement>().AsReadOnly());

            Actor.Invoking(x => x.AsksFor(TagName.Of(Locator))).Should().Throw<WaitingException<bool>>();
        }

        #endregion
    }
}
