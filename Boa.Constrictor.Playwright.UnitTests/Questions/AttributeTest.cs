using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright;
using Moq;
using NUnit.Framework;

namespace Boa.Constrictor.Playwright.UnitTests.Questions;

public class AttributeTest : BasePlaywrightLocatorQuestionTest
{
    [Test]
    public async Task TestGetAttribute()
    {
        // Arrange
        Locator
            .Setup(x => x.GetAttributeAsync(It.IsAny<string>(), It.IsAny<LocatorGetAttributeOptions>()))
            .Returns(Task.FromResult("abc123"));
        
        // Act
        var attribute = await Actor.AsksForAsync(Attribute.Of(PlaywrightLocator.Object, "my-attribute"));
        
        // Assert
        attribute.Should().Be("abc123");
    }
    
    [Test]
    public async Task TestNoAttributeFound()
    {
        // Act
        var attribute = await Actor.AsksForAsync(Attribute.Of(PlaywrightLocator.Object, "my-attribute"));
        
        // Assert
        attribute.Should().BeNull();
    }
}