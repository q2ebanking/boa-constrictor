using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright;
using Moq;
using NUnit.Framework;

namespace Boa.Constrictor.Playwright.UnitTests.Questions;

public class InnerTextTest : BasePlaywrightLocatorQuestionTest
{
    [Test]
    public async Task TestGetInnerText()
    {
        // Arrange
        Locator
            .Setup(x => x.InnerTextAsync(It.IsAny<LocatorInnerTextOptions>()))
            .Returns(Task.FromResult("Hello"));
        
        // Act
        var innerText = await Actor.AsksForAsync(InnerText.Of(PlaywrightLocator.Object));
        
        // Assert
        innerText.Should().Be("Hello");
    }
}