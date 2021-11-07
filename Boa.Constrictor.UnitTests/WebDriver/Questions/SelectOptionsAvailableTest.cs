using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class SelectOptionsAvailableTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestOneOption()
        {
            var option = new Mock<IWebElement>();
            option.SetupGet(x => x.Text).Returns("apple");

            var element = new Mock<IWebElement>();
            element.SetupGet(x => x.TagName).Returns("select");
            element.Setup(x => x.GetAttribute(It.IsAny<string>())).Returns<string>(null);
            element.Setup(x => x.FindElements(By.TagName("option"))).Returns(new List<IWebElement> { option.Object }.AsReadOnly());

            WebDriver.Setup(x => x.FindElement(It.IsAny<By>())).Returns(element.Object);
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());

            var options = Actor.AsksFor(SelectOptionsAvailable.For(Locator));
            options.Count.Should().Be(1);
            options[0].Should().Be("apple");
        }

        [Test]
        public void TestMultipleOptions()
        {
            var optionOne = new Mock<IWebElement>();
            optionOne.SetupGet(x => x.Text).Returns("apple");
            var optionTwo = new Mock<IWebElement>();
            optionTwo.SetupGet(x => x.Text).Returns("bee");
            var optionThree = new Mock<IWebElement>();
            optionThree.SetupGet(x => x.Text).Returns("cat");

            var element = new Mock<IWebElement>();
            element.SetupGet(x => x.TagName).Returns("select");
            element.Setup(x => x.GetAttribute(It.IsAny<string>())).Returns<string>(null);
            element.Setup(x => x.FindElements(By.TagName("option"))).Returns(
                new List<IWebElement> { 
                    optionOne.Object,
                    optionTwo.Object,
                    optionThree.Object
                }.AsReadOnly());

            WebDriver.Setup(x => x.FindElement(It.IsAny<By>())).Returns(element.Object);
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());

            var options = Actor.AsksFor(SelectOptionsAvailable.For(Locator));
            options.Count.Should().Be(3);
            options[0].Should().Be("apple");
            options[1].Should().Be("bee");
            options[2].Should().Be("cat");
        }

        [Test]
        public void TestNoOptions()
        {
            var element = new Mock<IWebElement>();
            element.SetupGet(x => x.TagName).Returns("select");
            element.Setup(x => x.GetAttribute(It.IsAny<string>())).Returns<string>(null);
            element.Setup(x => x.FindElements(By.TagName("option"))).Returns(new List<IWebElement>().AsReadOnly());

            WebDriver.Setup(x => x.FindElement(It.IsAny<By>())).Returns(element.Object);
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());

            var options = Actor.AsksFor(SelectOptionsAvailable.For(Locator));
            options.Count.Should().Be(0);
        }

        [Test]
        public void TestElementDoesNotExist()
        {
            SetUpFindElementsReturnsEmpty();

            Actor.Invoking(x => x.AsksFor(SelectOptionsAvailable.For(Locator))).Should().Throw<WaitingException<bool>>();
        }

        #endregion
    }
}
