using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class CountTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestOneElement()
        {
            Actor.AsksFor(Count.Of(Locator)).Should().Be(1);
        }

        [Test]
        public void TestZeroElements()
        {
            SetUpFindElementsReturnsEmpty();

            Actor.AsksFor(Count.Of(Locator)).Should().Be(0);
        }

        [Test]
        public void TestMultipleElements()
        {
            var elementOne = new Mock<IWebElement>();
            var elementTwo = new Mock<IWebElement>();
            var elementThree = new Mock<IWebElement>();
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>()))
                .Returns(new List<IWebElement> { elementOne.Object, elementTwo.Object, elementThree.Object }.AsReadOnly());

            Actor.AsksFor(Count.Of(Locator)).Should().Be(3);
        }

        #endregion
    }
}
