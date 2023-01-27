using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Boa.Constrictor.Selenium.UnitTests
{
    [TestFixture]
    public class WebLocatorTest
    {
        [Test]
        public void Equals_True()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new WebLocator("hello", By.Id("moto"));
            a.Equals(b).Should().BeTrue();
        }

        [Test]
        public void Equals_False_NonWebLocator()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new object();
            a.Equals(b).Should().BeFalse();
        }

        [Test]
        public void Equals_False_Description()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new WebLocator("goodbye", By.Id("moto"));
            a.Equals(b).Should().BeFalse();
        }

        [Test]
        public void Equals_False_QueryValue()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new WebLocator("hello", By.Id("goodbye"));
            a.Equals(b).Should().BeFalse();
        }

        [Test]
        public void Equals_False_QueryType()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new WebLocator("hello", By.CssSelector("moto"));
            a.Equals(b).Should().BeFalse();
        }

        [Test]
        public void FindElement()
        {
            var elementMock = new Mock<IWebElement>();
            elementMock.Setup(x => x.GetAttribute(It.IsAny<string>())).Returns("a");

            var driverMock = new Mock<IWebDriver>();
            driverMock.Setup(x => x.FindElement(It.IsAny<By>())).Returns(elementMock.Object);

            var locator = new WebLocator("hello", By.Id("moto"));
            var element = locator.FindElement(driverMock.Object);

            element.Should().NotBeNull();
            element.GetAttribute("id").Should().Be("a");
        }

        [Test]
        public void FindElements()
        {
            var elementMock = new Mock<IWebElement>();
            elementMock.Setup(x => x.GetAttribute(It.IsAny<string>())).Returns("a");

            var driverMock = new Mock<IWebDriver>();
            driverMock.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { elementMock.Object }.AsReadOnly());

            var locator = new WebLocator("hello", By.Id("moto"));
            var elements = locator.FindElements(driverMock.Object);

            elements.Should().NotBeNull();
            elements.Should().HaveCount(1);
            elements.First().GetAttribute("id").Should().Be("a");
        }

        [Test]
        public void GetHashCode_Same()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new WebLocator("hello", By.Id("moto"));
            int codeA = a.GetHashCode();
            int codeB = b.GetHashCode();
            codeA.Should().Be(codeB);
        }

        [Test]
        public void GetHashCode_Different_NonWebLocator()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new object();
            int codeA = a.GetHashCode();
            int codeB = b.GetHashCode();
            codeA.Should().NotBe(codeB);
        }

        [Test]
        public void GetHashCode_Different_Description()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new WebLocator("goodbye", By.Id("moto"));
            int codeA = a.GetHashCode();
            int codeB = b.GetHashCode();
            codeA.Should().NotBe(codeB);
        }

        [Test]
        public void GetHashCode_Different_QueryValue()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new WebLocator("hello", By.Id("goodbye"));
            int codeA = a.GetHashCode();
            int codeB = b.GetHashCode();
            codeA.Should().NotBe(codeB);
        }

        [Test]
        public void GetHashCode_Different_QueryType()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new WebLocator("hello", By.CssSelector("moto"));
            int codeA = a.GetHashCode();
            int codeB = b.GetHashCode();
            codeA.Should().NotBe(codeB);
        }
    }
}
