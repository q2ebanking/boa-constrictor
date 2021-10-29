using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class JavaScriptTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestStringScriptWithLocator()
        {
            var element = new Mock<IWebElement>();
            WebDriver.Setup(x => x.FindElement(It.IsAny<By>())).Returns(element.Object);
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());
            WebDriver.Setup(x => x.ExecuteScript(It.IsAny<string>(), It.IsAny<object[]>())).Returns("complete");

            Actor.Calls(JavaScript<string>.On(Locator, "execute some js")).Should().Be("complete");
            Logger.Messages.Should().ContainMatch("*JavaScript code to execute:");
            Logger.Messages.Should().ContainMatch("*execute some js");
            Logger.Messages.Should().ContainMatch("*JavaScript code arguments:");
            Logger.Messages.Should().ContainMatch("*IWebElement*");
        }

        [Test]
        public void TestScriptWithLocatorDoesNotExist()
        {
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement>().AsReadOnly());

            Actor.Invoking(x => x.Calls(JavaScript<string>.On(Locator, "execute some js"))).Should().Throw<WaitingException<bool>>();
        }

        [Test]
        public void TestIntScriptNoLocator()
        {
            var element = new Mock<IWebElement>();
            WebDriver.Setup(x => x.FindElement(It.IsAny<By>())).Returns(element.Object);
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());
            WebDriver.Setup(x => x.ExecuteScript(It.IsAny<string>(), It.IsAny<object[]>())).Returns(5);

            Actor.Calls(JavaScript<int>.OnPage("execute some js")).Should().Be(5);
            Logger.Messages.Should().ContainMatch("*JavaScript code to execute:");
            Logger.Messages.Should().ContainMatch("*execute some js");
        }

        [Test]
        public void TestBoolScriptWithLocatorAndArgs()
        {
            var element = new Mock<IWebElement>();
            WebDriver.Setup(x => x.FindElement(It.IsAny<By>())).Returns(element.Object);
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());
            WebDriver.Setup(x => x.ExecuteScript(It.IsAny<string>(), It.IsAny<object[]>())).Returns(false);

            var args = new object[] { "arg1", "arg2", "arg3" };
            Actor.Calls(JavaScript<bool>.On(Locator, "execute some js", args)).Should().BeFalse();
            Logger.Messages.Should().ContainMatch("*JavaScript code to execute:");
            Logger.Messages.Should().ContainMatch("*execute some js");
            Logger.Messages.Should().ContainMatch("*JavaScript code arguments:");
            Logger.Messages.Should().ContainMatch("*IWebElement*");
            Logger.Messages.Should().ContainMatch("*arg1");
            Logger.Messages.Should().ContainMatch("*arg2");
            Logger.Messages.Should().ContainMatch("*arg3");
        }

        [Test]
        public void TestScriptNoLocatorWithArgs()
        {
            var element = new Mock<IWebElement>();
            WebDriver.Setup(x => x.FindElement(It.IsAny<By>())).Returns(element.Object);
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());
            WebDriver.Setup(x => x.ExecuteScript(It.IsAny<string>(), It.IsAny<object[]>())).Returns(5);

            var args = new object[] { "arg1", "arg2", "arg3" };
            Actor.Calls(JavaScript<int>.OnPage("execute some js", args)).Should().Be(5);
            Logger.Messages.Should().ContainMatch("*JavaScript code to execute:");
            Logger.Messages.Should().ContainMatch("*execute some js");
            Logger.Messages.Should().ContainMatch("*JavaScript code arguments:");
            Logger.Messages.Should().ContainMatch("*arg1");
            Logger.Messages.Should().ContainMatch("*arg2");
            Logger.Messages.Should().ContainMatch("*arg3");
        }

        #endregion
    }
}
