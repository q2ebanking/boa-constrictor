using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright;
using Moq;
using NUnit.Framework;

namespace Boa.Constrictor.Playwright.UnitTests.Elements
{
    [TestFixture]
    public class PlaywrightLocatorTests
    {
        [Test]
        public void FindInTest()
        {
            // Arrange
            var mockLocator = new Mock<ILocator>();
            mockLocator
                .Setup(x => x.InnerTextAsync(It.IsAny<LocatorInnerTextOptions>()))
                .Returns(Task.FromResult("a"));
            
            var mockPage = new Mock<IPage>();
            mockPage
                .Setup(x => x.Locator(It.IsAny<string>(), It.IsAny<PageLocatorOptions>()))
                .Returns(mockLocator.Object);
            var locator = new PlaywrightLocator("Description", x => x.Locator("Test"));
            
            // Act
            var result = locator.FindIn(mockPage.Object);
            
            // Assert
            result.Should().BeEquivalentTo(mockLocator.Object);
            result.InnerTextAsync().Result.Should().Be("a");
        }
    }
}