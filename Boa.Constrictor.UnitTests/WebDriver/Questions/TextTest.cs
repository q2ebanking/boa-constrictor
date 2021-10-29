using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class TextTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestTextExists()
        {
            var element = new Mock<IWebElement>();
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());
            WebDriver.SetupGet(x => x.FindElement(It.IsAny<By>()).Text).Returns("some text");

            Actor.AsksFor(Text.Of(Locator)).Should().Be("some text");
        }

        [Test]
        public void TestTextNotExist()
        {
            var element = new Mock<IWebElement>();
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());
            WebDriver.SetupGet(x => x.FindElement(It.IsAny<By>()).Text).Returns(default(string));

            Actor.AsksFor(Text.Of(Locator)).Should().Be(null);
        }

        #endregion
    }
}
