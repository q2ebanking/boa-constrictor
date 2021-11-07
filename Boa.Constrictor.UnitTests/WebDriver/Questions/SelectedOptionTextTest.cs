using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class SelectedOptionTextTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestSelectedText()
        {
            var selectedOption = new Mock<IWebElement>();
            selectedOption.SetupGet(x => x.Selected).Returns(true);
            selectedOption.SetupGet(x => x.Text).Returns("zip");

            var element = new Mock<IWebElement>();
            element.SetupGet(x => x.TagName).Returns("select");
            element.Setup(x => x.GetAttribute(It.IsAny<string>())).Returns<string>(null);
            element.Setup(x => x.FindElements(By.TagName("option"))).Returns(new List<IWebElement> { selectedOption.Object }.AsReadOnly());

            WebDriver.Setup(x => x.FindElement(It.IsAny<By>())).Returns(element.Object);
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());

            Actor.AsksFor(SelectedOptionText.Of(Locator)).Should().Be("zip");
        }

        [Test]
        public void TestNoSelectTag()
        {
            var selectedOption = new Mock<IWebElement>();
            selectedOption.SetupGet(x => x.Selected).Returns(true);
            selectedOption.SetupGet(x => x.Text).Returns("zip");

            var element = new Mock<IWebElement>();
            element.SetupGet(x => x.TagName).Returns("wrong tag");
            element.Setup(x => x.GetAttribute(It.IsAny<string>())).Returns<string>(null);
            element.Setup(x => x.FindElements(By.TagName("option"))).Returns(new List<IWebElement> { selectedOption.Object }.AsReadOnly());

            WebDriver.Setup(x => x.FindElement(It.IsAny<By>())).Returns(element.Object);
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());

            Actor.Invoking(x => x.AsksFor(SelectedOptionText.Of(Locator))).Should().Throw<UnexpectedTagNameException>();
        }

        [Test]
        public void TestNoOptionSelected()
        {
            var selectedOption = new Mock<IWebElement>();
            selectedOption.SetupGet(x => x.Selected).Returns(false);
            selectedOption.SetupGet(x => x.Text).Returns("zip");

            var element = new Mock<IWebElement>();
            element.SetupGet(x => x.TagName).Returns("select");
            element.Setup(x => x.GetAttribute(It.IsAny<string>())).Returns<string>(null);
            element.Setup(x => x.FindElements(By.TagName("option"))).Returns(new List<IWebElement> { selectedOption.Object }.AsReadOnly());

            WebDriver.Setup(x => x.FindElement(It.IsAny<By>())).Returns(element.Object);
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());

            Actor.Invoking(x => x.AsksFor(SelectedOptionText.Of(Locator))).Should().Throw<NoSuchElementException>();
        }

        [Test]
        public void TestElementDoesNotExist()
        {
            SetUpFindElementsReturnsEmpty();

            Actor.Invoking(x => x.AsksFor(SelectedOptionText.Of(Locator))).Should().Throw<WaitingException<bool>>();
        }

        #endregion
    }
}
