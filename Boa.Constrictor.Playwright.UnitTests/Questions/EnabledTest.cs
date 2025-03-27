using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright;
using Moq;
using NUnit.Framework;

namespace Boa.Constrictor.Playwright.UnitTests.Questions;

public class EnabledTest : BasePlaywrightLocatorQuestionTest
{
    [Test]
    public async Task TestGetEnabled()
    {
        // Arrange
        Locator
            .Setup(x => x.IsEnabledAsync(It.IsAny<LocatorIsEnabledOptions>()))
            .Returns(Task.FromResult(false));
        
        // Act
        var enabled = await Actor.AsksForAsync(Enabled.Of(PlaywrightLocator.Object));
        
        // Assert
        enabled.Should().BeFalse();
    }
}