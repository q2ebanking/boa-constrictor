using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class EnabledStateTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestElementEnabled()
        {
            WebDriver.SetupGet(x => x.FindElement(It.IsAny<By>()).Enabled).Returns(true);

            Actor.AsksFor(EnabledState.Of(Locator)).Should().BeTrue();
        }

        [Test]
        public void TestElementDoesNotExist()
        {
            SetUpFindElementsReturnsEmpty();

            Actor.Invoking(x => x.AsksFor(EnabledState.Of(Locator))).Should().Throw<WaitingException<bool>>();
        }

        [Test]
        public void TestElementNotEnabled()
        {
            WebDriver.SetupGet(x => x.FindElement(It.IsAny<By>()).Enabled).Returns(false);

            Actor.AsksFor(EnabledState.Of(Locator)).Should().BeFalse();
        }

        #endregion
    }
}
