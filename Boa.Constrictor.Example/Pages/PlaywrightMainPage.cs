namespace Boa.Constrictor.Example;

using Boa.Constrictor.Playwright;
using Microsoft.Playwright;
using static Boa.Constrictor.Playwright.PlaywrightLocator;

public class PlaywrightMainPage
{
    public const string Url = "https://en.wikipedia.org/wiki/Main_Page";

    public static IPlaywrightLocator SearchButton => L("Wikipedia Search Button", page => page.Locator("button", new PageLocatorOptions() { HasText = "Search" }));
    public static IPlaywrightLocator SearchInput => L("Wikipedia Search Button", "#searchInput");
    public static IPlaywrightLocator Title => L("Page Heading", page => page.Locator("#firstHeading").Locator("span"));
}