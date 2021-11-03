using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class JavaScriptTextTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestScriptText()
        {
            WebDriver.Setup(x => x.ExecuteScript(It.IsAny<string>(), It.IsAny<object[]>())).Returns("textContent");

            Actor.Calls(JavaScriptText.Of(Locator)).Should().Be("textContent");
            Logger.Messages.Should().ContainMatch("*JavaScript code to execute:");
            Logger.Messages.Should().ContainMatch("*return arguments[0].textContent;");
            Logger.Messages.Should().ContainMatch("*JavaScript code arguments:");
            Logger.Messages.Should().ContainMatch("*IWebElement*");
        }

        [Test]
        public void TestScriptWithLocatorDoesNotExist()
        {
            SetUpFindElementsReturnsEmpty();

            Actor.Invoking(x => x.Calls(JavaScriptText.Of(Locator))).Should().Throw<WaitingException<bool>>();
        }

        #endregion
    }
}
