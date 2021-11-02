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
    public class ValueAttributeListTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestSingleElement()
        {
            var element = new Mock<IWebElement>();
            element.Setup(x => x.GetAttribute(It.IsAny<string>())).Returns("a");
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());

            List<string> list = Actor.AsksFor(ValueAttributeList.For(Locator)).ToList();
            list.Count.Should().Be(1);
            list[0].Should().Be("a");
        }

        [Test]
        public void TestMultipleElements()
        {
            var elementOne = new Mock<IWebElement>();
            elementOne.Setup(x => x.GetAttribute(It.IsAny<string>())).Returns("a");
            var elementTwo = new Mock<IWebElement>();
            elementTwo.Setup(x => x.GetAttribute(It.IsAny<string>())).Returns("b");
            var elementThree = new Mock<IWebElement>();
            elementThree.Setup(x => x.GetAttribute(It.IsAny<string>())).Returns("c");
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>()))
                .Returns(new List<IWebElement>
                {
                    elementOne.Object,
                    elementTwo.Object,
                    elementThree.Object
                }.AsReadOnly());

            var list = Actor.AsksFor(ValueAttributeList.For(Locator)).ToList();
            list.Count.Should().Be(3);
            list[0].Should().Be("a");
            list[1].Should().Be("b");
            list[2].Should().Be("c");
        }

        [Test]
        public void TestNoElements()
        {
            SetUpFindElementsReturnsEmpty();

            Actor.Invoking(x => x.AsksFor(ValueAttributeList.For(Locator))).Should().Throw<WaitingException<bool>>();
        }

        #endregion
    }
}
