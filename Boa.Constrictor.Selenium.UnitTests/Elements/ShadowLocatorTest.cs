using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Boa.Constrictor.Selenium.UnitTests
{
    [TestFixture]
    public class ShadowLocatorTest
    {
        [Test]
        public void Constructor_Throws_WhenHostIsNotCss()
        {
            Action act = () => new ShadowLocator("hello", By.Id("host"), By.CssSelector("input"));

            act.Should().Throw<ArgumentException>()
                .WithMessage("Shadow host locator must be a CSS selector.*");
        }

        [Test]
        public void Equals_True()
        {
            var a = new ShadowLocator("hello", By.CssSelector("my-host"), By.CssSelector("input"));
            var b = new ShadowLocator("hello", By.CssSelector("my-host"), By.CssSelector("input"));

            a.Equals(b).Should().BeTrue();
        }

        [Test]
        public void Equals_False_NonShadowLocator()
        {
            var a = new ShadowLocator("hello", By.CssSelector("my-host"), By.CssSelector("input"));
            var b = new object();

            a.Equals(b).Should().BeFalse();
        }

        [Test]
        public void Equals_False_Description()
        {
            var a = new ShadowLocator("hello", By.CssSelector("my-host"), By.CssSelector("input"));
            var b = new ShadowLocator("goodbye", By.CssSelector("my-host"), By.CssSelector("input"));

            a.Equals(b).Should().BeFalse();
        }

        [Test]
        public void Equals_False_ShadowHost()
        {
            var a = new ShadowLocator("hello", By.CssSelector("my-host"), By.CssSelector("input"));
            var b = new ShadowLocator("hello", By.CssSelector("other-host"), By.CssSelector("input"));

            a.Equals(b).Should().BeFalse();
        }

        [Test]
        public void Equals_False_ShadowDescendant()
        {
            var a = new ShadowLocator("hello", By.CssSelector("my-host"), By.CssSelector("input"));
            var b = new ShadowLocator("hello", By.CssSelector("my-host"), By.CssSelector("button"));

            a.Equals(b).Should().BeFalse();
        }

        [Test]
        public void FindElement()
        {
            var host = new Mock<IWebElement>();
            var element = new Mock<IWebElement>();
            element.Setup(x => x.GetAttribute(It.IsAny<string>())).Returns("a");

            var shadowRoot = new Mock<ISearchContext>();
            shadowRoot.Setup(x => x.FindElement(It.IsAny<By>())).Returns(element.Object);

            var driver = new Mock<IWebDriver>();
            driver.Setup(x => x.FindElement(It.IsAny<By>())).Returns(host.Object);
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.IsAny<string>(), It.IsAny<object[]>()))
                .Returns(shadowRoot.Object);

            var locator = new ShadowLocator("hello", By.CssSelector("my-host"), By.CssSelector("input"));
            var found = locator.FindElement(driver.Object);

            found.Should().NotBeNull();
            found.GetAttribute("id").Should().Be("a");
        }

        [Test]
        public void FindElements()
        {
            var host = new Mock<IWebElement>();
            var element = new Mock<IWebElement>();
            element.Setup(x => x.GetAttribute(It.IsAny<string>())).Returns("a");

            var shadowRoot = new Mock<ISearchContext>();
            shadowRoot.Setup(x => x.FindElements(It.IsAny<By>()))
                .Returns(new List<IWebElement> { element.Object }.AsReadOnly());

            var driver = new Mock<IWebDriver>();
            driver.Setup(x => x.FindElement(It.IsAny<By>())).Returns(host.Object);
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.IsAny<string>(), It.IsAny<object[]>()))
                .Returns(shadowRoot.Object);

            var locator = new ShadowLocator("hello", By.CssSelector("my-host"), By.CssSelector("input"));
            var found = locator.FindElements(driver.Object);

            found.Should().NotBeNull();
            found.Should().HaveCount(1);
            found.First().GetAttribute("id").Should().Be("a");
        }

        [Test]
        public void FindElement_Throws_WhenDriverIsNotJavaScriptExecutor()
        {
            var driver = new Mock<IWebDriver>();
            var locator = new ShadowLocator("hello", By.CssSelector("my-host"), By.CssSelector("input"));

            Action act = () => locator.FindElement(driver.Object);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("The WebDriver must implement IJavaScriptExecutor to access shadow roots.");
        }

        [Test]
        public void FindElement_Throws_WhenShadowRootIsNotOpen()
        {
            var host = new Mock<IWebElement>();

            var driver = new Mock<IWebDriver>();
            driver.Setup(x => x.FindElement(It.IsAny<By>())).Returns(host.Object);
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.IsAny<string>(), It.IsAny<object[]>()))
                .Returns((object)null);

            var locator = new ShadowLocator("hello", By.CssSelector("my-host"), By.CssSelector("input"));

            Action act = () => locator.FindElement(driver.Object);

            act.Should().Throw<NoSuchElementException>()
                .WithMessage("Shadow host 'hello' does not expose an open shadow root.*");
        }

        [Test]
        public void GetHashCode_Same()
        {
            var a = new ShadowLocator("hello", By.CssSelector("my-host"), By.CssSelector("input"));
            var b = new ShadowLocator("hello", By.CssSelector("my-host"), By.CssSelector("input"));

            a.GetHashCode().Should().Be(b.GetHashCode());
        }

        [Test]
        public void GetHashCode_Different_Description()
        {
            var a = new ShadowLocator("hello", By.CssSelector("my-host"), By.CssSelector("input"));
            var b = new ShadowLocator("goodbye", By.CssSelector("my-host"), By.CssSelector("input"));

            a.GetHashCode().Should().NotBe(b.GetHashCode());
        }

        [Test]
        public void GetHashCode_Different_ShadowHost()
        {
            var a = new ShadowLocator("hello", By.CssSelector("my-host"), By.CssSelector("input"));
            var b = new ShadowLocator("hello", By.CssSelector("other-host"), By.CssSelector("input"));

            a.GetHashCode().Should().NotBe(b.GetHashCode());
        }

        [Test]
        public void GetHashCode_Different_ShadowDescendant()
        {
            var a = new ShadowLocator("hello", By.CssSelector("my-host"), By.CssSelector("input"));
            var b = new ShadowLocator("hello", By.CssSelector("my-host"), By.CssSelector("button"));

            a.GetHashCode().Should().NotBe(b.GetHashCode());
        }
    }
}
