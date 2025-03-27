using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright;
using Moq;
using NUnit.Framework;

namespace Boa.Constrictor.Playwright.UnitTests.Questions;

public class VisibilityTest : BasePlaywrightLocatorQuestionTest
{
    [Test]
    public async Task TestGetVisibility()
    {
        // Arrange
        Locator
            .Setup(x => x.IsVisibleAsync(It.IsAny<LocatorIsVisibleOptions>()))
            .Returns(Task.FromResult(true));
        
        // Act
        var visibility = await Actor.AsksForAsync(Visibility.Of(PlaywrightLocator.Object));
        
        // Assert
        visibility.Should().BeTrue();
    }
}