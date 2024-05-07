namespace Boa.Constrictor.Example;

using System.Threading.Tasks;
using Boa.Constrictor.Playwright;
using Boa.Constrictor.Playwright.Abilities;
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
        Actor = new Actor("Pantz", new ConsoleLogger());
        Actor.Can(await BrowseTheWebSynchronously.UsingChromium(options));
    }

    [Test]
    public async Task TestWikipediaSearch()
    {
        await Actor.AttemptsToAsync(OpenNewPage.ToUrl(PlaywrightMainPage.Url));
        var valueAttribute = await Actor.AskingForAsync(Attribute.Of(PlaywrightMainPage.SearchInput, "value"));
        valueAttribute.Should().BeNullOrEmpty();

        await Actor.AttemptsToAsync(Fill.ValueTo(PlaywrightMainPage.SearchInput, "Giant panda"));
        await Actor.AttemptsToAsync(Click.On(PlaywrightMainPage.SearchButton));

        await Actor.Expects(PlaywrightMainPage.Title).ToHaveTextAsync("Giant panda");
        // var heading = await Actor.AsksForAsync(Text.Of(PlaywrightMainPage.Title));
        // heading.Should().Be("Giant panda");


        // Todo: add support to wait for async questions
        // Actor.WaitsUntil(Text.Of("[id='firstHeading'] span"), IsEqualTo.Value("Giant panda"));
    }
}