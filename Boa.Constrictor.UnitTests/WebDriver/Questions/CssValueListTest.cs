using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class CssValueListTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestSingleElement()
        {
            var element = new Mock<IWebElement>();
            element.Setup(x => x.GetCssValue(It.IsAny<string>())).Returns("red");
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());

            var list = Actor.AsksFor(CssValueList.For(Locator, "color")).ToList();
            list.Count.Should().Be(1);
            list[0].Should().Be("red");
        }

        [Test]
        public void TestMultipleElements()
        {
            var elementOne = new Mock<IWebElement>();
            elementOne.Setup(x => x.GetCssValue(It.IsAny<string>())).Returns("red");
            var elementTwo = new Mock<IWebElement>();
            elementTwo.Setup(x => x.GetCssValue(It.IsAny<string>())).Returns("green");
            var elementThree = new Mock<IWebElement>();
            elementThree.Setup(x => x.GetCssValue(It.IsAny<string>())).Returns("blue");
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>()))
                .Returns(new List<IWebElement>
                {
                    elementOne.Object,
                    elementTwo.Object,
                    elementThree.Object
                }.AsReadOnly());

            var list = Actor.AsksFor(CssValueList.For(Locator, "color")).ToList();
            list.Count.Should().Be(3);
            list[0].Should().Be("red");
            list[1].Should().Be("green");
            list[2].Should().Be("blue");
        }

        [Test]
        public void TestNoElements()
        {
            SetUpFindElementsReturnsEmpty();

            Actor.Invoking(x => x.AsksFor(CssValueList.For(Locator, "color"))).Should().Throw<WaitingException<bool>>();
        }

        #endregion
    }
}
