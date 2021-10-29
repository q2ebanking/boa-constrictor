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
            var element = new Mock<IWebElement>();
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());
            WebDriver.SetupGet(x => x.FindElement(It.IsAny<By>()).Size).Returns(new Size(1, 2));

            var point = Actor.AsksFor(PixelSize.Of(Locator));
            point.Width.Should().Be(1);
            point.Height.Should().Be(2);
        }

        #endregion
    }
}
