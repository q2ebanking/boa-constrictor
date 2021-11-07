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
    public class IdAttributeListTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestSingleElement()
        {
            var element = new Mock<IWebElement>();
            element.Setup(x => x.GetAttribute(It.IsAny<string>())).Returns("a_button");
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());

            List<string> list = Actor.AsksFor(IdAttributeList.For(Locator)).ToList();
            list.Count.Should().Be(1);
            list[0].Should().Be("a_button");
        }

        [Test]
        public void TestMultipleElements()
        {
            var elementOne = new Mock<IWebElement>();
            elementOne.Setup(x => x.GetAttribute(It.IsAny<string>())).Returns("a_button");
            var elementTwo = new Mock<IWebElement>();
            elementTwo.Setup(x => x.GetAttribute(It.IsAny<string>())).Returns("b_button");
            var elementThree = new Mock<IWebElement>();
            elementThree.Setup(x => x.GetAttribute(It.IsAny<string>())).Returns("c_button");
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>()))
                .Returns(new List<IWebElement>
                {
                    elementOne.Object,
                    elementTwo.Object,
                    elementThree.Object
                }.AsReadOnly());

            var list = Actor.AsksFor(IdAttributeList.For(Locator)).ToList();
            list.Count.Should().Be(3);
            list[0].Should().Be("a_button");
            list[1].Should().Be("b_button");
            list[2].Should().Be("c_button");
        }

        [Test]
        public void TestNoElements()
        {
            SetUpFindElementsReturnsEmpty();

            Actor.Invoking(x => x.AsksFor(IdAttributeList.For(Locator))).Should().Throw<WaitingException<bool>>();
        }

        #endregion
    }
}
