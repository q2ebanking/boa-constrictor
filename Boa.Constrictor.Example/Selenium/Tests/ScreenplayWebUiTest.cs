using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Selenium;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace Boa.Constrictor.Example
{
    public class ScreenplayWebUiTest
    {
        private IActor Actor;

        [SetUp]
        public void InitializeScreenplay()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("headless");                  // Remove this line to "see" the browser run
            options.AddArgument("window-size=1920,1080");     // Use this option with headless mode
            ChromeDriver driver = new ChromeDriver(options);

            Actor = new Actor(name: "Andy", logger: new ConsoleLogger());
            Actor.Can(BrowseTheWeb.With(driver));
        }

        [TearDown]
        public void QuitBrowser()
        {
            Actor.AttemptsTo(QuitWebDriver.ForBrowser());
        }

        [Test]
        public void TestWikipediaSearch()
        {
            Actor.AttemptsTo(Navigate.ToUrl(MainPage.Url));
            Actor.AskingFor(ValueAttribute.Of(MainPage.SearchInput)).Should().BeEmpty();
            Actor.AttemptsTo(SearchWikipedia.For("Giant panda"));
            Actor.WaitsUntil(Text.Of(ArticlePage.Title), IsEqualTo.Value("Giant panda"));
        }
    }
}
