using Boa.Constrictor.Playwright.Elements;
using FluentAssertions;
using Microsoft.Playwright;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Boa.Constrictor.Playwright.UnitTests.Elements
{
    [TestFixture]
    public class BoaWebLocatorTests
    {
        private Mock<IPage> _mockPage;
        private Mock<IFrame> _mockFrame;
        private Mock<ILocator> _mockLocator;

        [SetUp]
        public void SetUp()
        {
            _mockLocator = new Mock<ILocator>();
            _mockPage = new Mock<IPage>();
            _mockFrame = new Mock<IFrame>();

            _mockFrame.SetupGet(x => x.Name).Returns("test-frame");
            _mockPage.Setup(x => x.Locator(It.IsAny<string>(), It.IsAny<PageLocatorOptions>()))
                .Returns(_mockLocator.Object);

            _mockPage.Setup(x => x.Frame(It.IsAny<string>()))
                .Returns(_mockFrame.Object);

            _mockFrame.Setup(x => x.Locator(It.IsAny<string>(), It.IsAny<FrameLocatorOptions>()))
                .Returns(_mockLocator.Object);
        }

        [Test]
        public void L_WithEmptyDescription_UsesQueryMechanismAsDescription()
        {
            // Arrange
            var baseLocator = new BoaWebLocator("test", By.Id("testId"));
            var query = By.Id("newId");

            // Act
            var result = baseLocator.L(query, "");

            // Assert
            result.Description.Should().Be($"{query.Mechanism}:\n{query.Criteria}\n");
        }

        [Test]
        public void FindIn_WithPageHavingNoFrames_ThrowsArgumentNullException()
        {
            // Arrange
            var boaLocator = new BoaWebLocator("test", By.Id("testId"));
            var emptyFramesList = new List<IFrame>();
            _mockPage.SetupGet(x => x.Frames).Returns(emptyFramesList);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ((IPlaywrightLocator)boaLocator).FindIn(_mockPage.Object));
        }

        [Test]
        public void FindIn_WithPageHavingFrames_UsesFirstFrame()
        {
            // Arrange
            var boaLocator = new BoaWebLocator("test", By.Id("testId"));
            var mockFrame1 = new Mock<IFrame>();
            mockFrame1.SetupGet(x => x.Name).Returns("first-frame");
            var mockFrame2 = new Mock<IFrame>();
            mockFrame2.SetupGet(x => x.Name).Returns("second-frame");

            var framesList = new List<IFrame> { mockFrame1.Object, mockFrame2.Object };
            _mockPage.SetupGet(x => x.Frames).Returns(framesList);

            // Act
            var result = ((IPlaywrightLocator)boaLocator).FindIn(_mockPage.Object);

            // Assert
            _mockPage.Verify(x => x.Frame("first-frame"), Times.Once);
            _mockPage.Verify(x => x.Frame("second-frame"), Times.Never);
            result.Should().Be(_mockLocator.Object);
        }

        [Test]
        public void ConvertSeleniumByToPlaywrightSelector_WithId_ReturnsCorrectSelector()
        {
            // Arrange
            var boaLocator = new BoaWebLocator("test", By.Id("testId"));
            var mockFrame1 = new Mock<IFrame>();
            mockFrame1.SetupGet(x => x.Name).Returns("frame1");
            var framesList = new List<IFrame> { mockFrame1.Object };
            _mockPage.SetupGet(x => x.Frames).Returns(framesList);

            // Act
            var result = ((IPlaywrightLocator)boaLocator).FindIn(_mockPage.Object);

            // Assert
            result.Should().Be(_mockLocator.Object);
        }

        [Test]
        public void ConvertSeleniumByToPlaywrightSelector_WithClassName_ReturnsCorrectSelector()
        {
            // Arrange
            var boaLocator = new BoaWebLocator("test", By.ClassName("test-class"));
            var mockFrame1 = new Mock<IFrame>();
            mockFrame1.SetupGet(x => x.Name).Returns("frame1");
            var framesList = new List<IFrame> { mockFrame1.Object };
            _mockPage.SetupGet(x => x.Frames).Returns(framesList);

            // Act
            var result = ((IPlaywrightLocator)boaLocator).FindIn(_mockPage.Object);

            // Assert
            result.Should().Be(_mockLocator.Object);
        }

        [Test]
        public void ConvertSeleniumByToPlaywrightSelector_WithTagName_ReturnsCorrectSelector()
        {
            // Arrange
            var boaLocator = new BoaWebLocator("test", By.TagName("div"));
            var mockFrame1 = new Mock<IFrame>();
            mockFrame1.SetupGet(x => x.Name).Returns("frame1");
            var framesList = new List<IFrame> { mockFrame1.Object };
            _mockPage.SetupGet(x => x.Frames).Returns(framesList);

            // Act
            var result = ((IPlaywrightLocator)boaLocator).FindIn(_mockPage.Object);

            // Assert
            result.Should().Be(_mockLocator.Object);
        }

        [Test]
        public void ConvertSeleniumByToPlaywrightSelector_WithName_ReturnsCorrectSelector()
        {
            // Arrange
            var boaLocator = new BoaWebLocator("test", By.Name("test-name"));
            var mockFrame1 = new Mock<IFrame>();
            mockFrame1.SetupGet(x => x.Name).Returns("frame1");
            var framesList = new List<IFrame> { mockFrame1.Object };
            _mockPage.SetupGet(x => x.Frames).Returns(framesList);

            // Act
            var result = ((IPlaywrightLocator)boaLocator).FindIn(_mockPage.Object);

            // Assert
            result.Should().Be(_mockLocator.Object);
        }

        [Test]
        public void ConvertSeleniumByToPlaywrightSelector_WithCssSelector_ReturnsCorrectSelector()
        {
            // Arrange
            var boaLocator = new BoaWebLocator("test", By.CssSelector(".custom-selector"));
            var mockFrame1 = new Mock<IFrame>();
            mockFrame1.SetupGet(x => x.Name).Returns("frame1");
            var framesList = new List<IFrame> { mockFrame1.Object };
            _mockPage.SetupGet(x => x.Frames).Returns(framesList);

            // Act
            var result = ((IPlaywrightLocator)boaLocator).FindIn(_mockPage.Object);

            // Assert
            result.Should().Be(_mockLocator.Object);
        }

        [Test]
        public void ConvertSeleniumByToPlaywrightSelector_WithXPath_ReturnsCorrectSelector()
        {
            // Arrange
            var boaLocator = new BoaWebLocator("test", By.XPath("//div[@id='test']"));
            var mockFrame1 = new Mock<IFrame>();
            mockFrame1.SetupGet(x => x.Name).Returns("frame1");
            var framesList = new List<IFrame> { mockFrame1.Object };
            _mockPage.SetupGet(x => x.Frames).Returns(framesList);

            // Act
            var result = ((IPlaywrightLocator)boaLocator).FindIn(_mockPage.Object);

            // Assert
            result.Should().Be(_mockLocator.Object);
        }

        [Test]
        public void ConvertSeleniumByToPlaywrightSelector_WithLinkText_ReturnsCorrectSelector()
        {
            // Arrange
            var boaLocator = new BoaWebLocator("test", By.LinkText("Click here"));
            var mockFrame1 = new Mock<IFrame>();
            mockFrame1.SetupGet(x => x.Name).Returns("frame1");
            var framesList = new List<IFrame> { mockFrame1.Object };
            _mockPage.SetupGet(x => x.Frames).Returns(framesList);

            // Act
            var result = ((IPlaywrightLocator)boaLocator).FindIn(_mockPage.Object);

            // Assert
            result.Should().Be(_mockLocator.Object);
        }

        [Test]
        public void ConvertSeleniumByToPlaywrightSelector_WithPartialLinkText_ReturnsCorrectSelector()
        {
            // Arrange
            var boaLocator = new BoaWebLocator("test", By.PartialLinkText("Click"));
            var mockFrame1 = new Mock<IFrame>();
            mockFrame1.SetupGet(x => x.Name).Returns("frame1");
            var framesList = new List<IFrame> { mockFrame1.Object };
            _mockPage.SetupGet(x => x.Frames).Returns(framesList);

            // Act
            var result = ((IPlaywrightLocator)boaLocator).FindIn(_mockPage.Object);

            // Assert
            result.Should().Be(_mockLocator.Object);
        }

        [Test]
        public void L_WithDifferentByTypes_CreatesCorrectLocators()
        {
            // Arrange
            var baseLocator = new BoaWebLocator("test", By.Id("testId"));

            // Test various By types
            var byId = By.Id("elementId");
            var byClassName = By.ClassName("class-name");
            var byTagName = By.TagName("div");
            var byXPath = By.XPath("//div[@id='test']");
            var byCssSelector = By.CssSelector(".selector");

            // Act
            var locatorId = baseLocator.L(byId, "ID Locator");
            var locatorClass = baseLocator.L(byClassName, "Class Locator");
            var locatorTag = baseLocator.L(byTagName, "Tag Locator");
            var locatorXPath = baseLocator.L(byXPath, "XPath Locator");
            var locatorCss = baseLocator.L(byCssSelector, "CSS Locator");

            // Assert Selenium locators work
            locatorId.Query.Should().Be(byId);
            locatorClass.Query.Should().Be(byClassName);
            locatorTag.Query.Should().Be(byTagName);
            locatorXPath.Query.Should().Be(byXPath);
            locatorCss.Query.Should().Be(byCssSelector);
        }
    }
}