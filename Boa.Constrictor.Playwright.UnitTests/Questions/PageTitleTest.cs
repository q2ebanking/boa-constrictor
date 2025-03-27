using System.Threading.Tasks;
using Boa.Constrictor.Screenplay;
using FluentAssertions;
using Microsoft.Playwright;
using Moq;
using NUnit.Framework;

namespace Boa.Constrictor.Playwright.UnitTests.Questions;

public class PageTitleTest : BasePlaywrightQuestionTest
{
    [Test]
    public async Task TestGetPageTitle()
    {
        // Arrange
        Page.Setup(x => x.TitleAsync()).Returns(Task.FromResult("abc123"));
        
        // Act
        var title = await Actor.AsksForAsync(PageTitle.OfPage());
        
        // Assert
        title.Should().Be("abc123");
    }
}