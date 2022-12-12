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
            options.AddArgument("headless");   // Remove this line to "see" the browser run
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
        public void TestDuckDuckGoWebSearch()
        {
            Actor.AttemptsTo(Navigate.ToUrl(SearchPage.Url));
            Actor.AskingFor(ValueAttribute.Of(SearchPage.SearchInput)).Should().BeEmpty();
            Actor.AttemptsTo(SearchDuckDuckGo.For("panda"));
            Actor.WaitsUntil(Appearance.Of(ResultPage.ResultLinks), IsEqualTo.True());
        }
    }
}
