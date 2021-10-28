using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class AlertPresenceTest : BaseWebQuestionTest
    {
        #region Tests

        [Test]
        public void TestAlertPresent()
        {
            WebDriver.Setup(x => x.SwitchTo().Alert());

            Actor.AsksFor(AlertPresence.InBrowser()).Should().BeTrue();
            WebDriver.Verify(x => x.SwitchTo().Alert(), Times.Once);
        }

        [Test]
        public void TestAlertNotPresent()
        {
            WebDriver.Setup(x => x.SwitchTo().Alert()).Throws(new NoAlertPresentException());

            Actor.AsksFor(AlertPresence.InBrowser()).Should().BeFalse();
            WebDriver.Verify(x => x.SwitchTo().Alert(), Times.Once);
        }

        #endregion
    }
}
