using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Selenium;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class TextListTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestSingleElement()
        {
            var element = new Mock<IWebElement>();
            element.SetupGet(x => x.Text).Returns("apple");
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());

            var list = Actor.AsksFor(TextList.For(Locator)).ToList();
            list.Count.Should().Be(1);
            list[0].Should().Be("apple");
        }

        [Test]
        public void TestMultipleElements()
        {
            var elementOne = new Mock<IWebElement>();
            elementOne.SetupGet(x => x.Text).Returns("apple");
            var elementTwo = new Mock<IWebElement>();
            elementTwo.SetupGet(x => x.Text).Returns("bee");
            var elementThree = new Mock<IWebElement>();
            elementThree.SetupGet(x => x.Text).Returns("cat");
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>()))
                .Returns(new List<IWebElement> 
                { 
                    elementOne.Object,
                    elementTwo.Object,
                    elementThree.Object
                }.AsReadOnly());

            var list = Actor.AsksFor(TextList.For(Locator)).ToList();
            list.Count.Should().Be(3);
            list[0].Should().Be("apple");
            list[1].Should().Be("bee");
            list[2].Should().Be("cat");
        }

        [Test]
        public void TestNoElements()
        {
            SetUpFindElementsReturnsEmpty();

            Actor.Invoking(x => x.AsksFor(TextList.For(Locator))).Should().Throw<WaitingException<bool>>();
        }

        #endregion
    }
}
