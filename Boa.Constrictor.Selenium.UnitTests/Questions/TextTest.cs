using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Selenium;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Boa.Constrictor.Selenium.UnitTests
{
    public class TextTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [TestCase("some text")]
        [TestCase("")]
        public void TestGetText(string elementText)
        {
            WebDriver.SetupGet(x => x.FindElement(It.IsAny<By>()).Text).Returns(elementText);

            Actor.AsksFor(Text.Of(Locator)).Should().Be(elementText);
        }

        [Test]
        public void TestElementDoesNotExist()
        {
            SetUpFindElementsReturnsEmpty();

            Actor.Invoking(x => x.AsksFor(Text.Of(Locator))).Should().Throw<WaitingException<bool>>();
        }

        #endregion
    }
}
