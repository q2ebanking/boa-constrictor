namespace Boa.Constrictor.Example;

using System.Threading.Tasks;
using Boa.Constrictor.Playwright.Abilities;
using Boa.Constrictor.Playwright.Questions;
using Boa.Constrictor.Playwright.Tasks;
using Boa.Constrictor.Screenplay;
using FluentAssertions;
using Microsoft.Playwright;
using NUnit.Framework;

public class ScreenplayPlaywrightTest
{
    private IActor Actor;

    [SetUp]
    public async Task Setup()
    {
        var options = new BrowserTypeLaunchOptions()
        {
            Headless = false
        };
        Actor = new Actor(name: "Keith", logger: new ConsoleLogger());
        Actor.Can(await BrowseTheWebSynchronously.UsingChromium(options));
    }

    [Test]
    public async Task TestWikipediaSearch()
    {
        await Actor.AttemptsToAsync(OpenNewPage.ToUrl(MainPage.Url));
        var valueAttribute = await Actor.AskingForAsync(Attribute.Of("[name='search']", "value"));
        valueAttribute.Should().BeNullOrEmpty();

        await Actor.AttemptsToAsync(Fill.ValueTo("[name='search']", "Giant panda"));
        await Actor.AttemptsToAsync(Click.On("//button[text()='Search']"));

        var heading = await Actor.AsksForAsync(Text.Of("[id='firstHeading'] span"));
        heading.Should().Be("Giant panda");

        // Todo: add support to wait for async questions
        // Actor.WaitsUntil(Text.Of("[id='firstHeading'] span"), IsEqualTo.Value("Giant panda"));
    }
}