using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Drawing;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class PixelSizeTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestSize()
        {
            WebDriver.SetupGet(x => x.FindElement(It.IsAny<By>()).Size).Returns(new Size(100, 200));

            var point = Actor.AsksFor(PixelSize.Of(Locator));
            point.Width.Should().Be(100);
            point.Height.Should().Be(200);
        }

        [Test]
        public void TestElementDoesNotExist()
        {
            SetUpFindElementsReturnsEmpty();

            Actor.Invoking(x => x.AsksFor(PixelSize.Of(Locator))).Should().Throw<WaitingException<bool>>();
        }

        #endregion
    }
}
