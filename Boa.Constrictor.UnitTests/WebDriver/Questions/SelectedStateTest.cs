using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class SelectedStateTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestElementSelected()
        {
            WebDriver.SetupGet(x => x.FindElement(It.IsAny<By>()).Selected).Returns(true);

            Actor.AsksFor(SelectedState.Of(Locator)).Should().BeTrue();
        }

        [Test]
        public void TestElementDoesNotExist()
        {
            SetUpFindElementsReturnsEmpty();

            Actor.Invoking(x => x.AsksFor(SelectedState.Of(Locator))).Should().Throw<WaitingException<bool>>();
        }

        [Test]
        public void TestElementNotSelected()
        {
            WebDriver.SetupGet(x => x.FindElement(It.IsAny<By>()).Selected).Returns(false);

            Actor.AsksFor(SelectedState.Of(Locator)).Should().BeFalse();
        }

        #endregion
    }
}
