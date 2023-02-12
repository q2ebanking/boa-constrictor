using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Selenium;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static Boa.Constrictor.Selenium.WebLocator;
namespace Boa.Constrictor.Example
{
    public class ScreenplayWebUiShadowRootTest
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

        // Reference: See the image that visualizes the relationship between the DOM and the Shadow DOM https://www.lambdatest.com/blog/shadow-dom-in-selenium/

        [Test]
        public void GetShadowRoot()
        {
            Actor.AttemptsTo(Navigate.ToUrl(ScreenplayWebUiShadowRootTest.Url));

            ISearchContext shadowRoot = Actor.AskingFor(Selenium.ShadowRoot.ForShadowHost(ShadowHostDiv));

            shadowRoot.Should().NotBeNull();
        }

        [Test]
        public void NonShadowNull()
        {
            Actor.AttemptsTo(Navigate.ToUrl(Url));

            ISearchContext shadowRoot = Actor.AskingFor(Selenium.ShadowRoot.ForShadowHost(NonShadowHostDiv));

            shadowRoot.Should().BeNull();
        }

        [Test]
        public void ShadowSpanText()
        {
            Actor.AttemptsTo(Navigate.ToUrl(Url));

            ISearchContext shadowRoot = Actor.AskingFor(Selenium.ShadowRoot.ForShadowHost(ShadowHostDiv));

            IWebElement infoSpan = shadowRoot.FindElement(By.ClassName("info"));
            infoSpan.Text.Should().Be("some text");
        }

        [Test]
        public void NestedShadowDivText()
        {
            Actor.AttemptsTo(Navigate.ToUrl(Url));

            ISearchContext shadowRoot = Actor.AskingFor(Selenium.ShadowRoot.ForShadowHost(ShadowHostDiv));
            IWebElement nestedShadowHostDiv = shadowRoot.FindElement(By.CssSelector("#nested_shadow_host"));

            ISearchContext nestedShadowHostDivShadowRoot = nestedShadowHostDiv.GetShadowRoot();
            IWebElement nestedShadowContentDiv = nestedShadowHostDivShadowRoot.FindElement(By.Id("nested_shadow_content"));
            string nestedShadowHostContentDivText = nestedShadowContentDiv.FindElement(By.TagName("div")).Text;

            nestedShadowHostContentDivText.Should().Be("nested text");
        }

        #region Locators

        public const string Url = "http://watir.com/examples/shadow_dom.html";

        public static IWebLocator NonShadowHostDiv => L(
            "ShadowHostDiv",
            By.Id("non_host"));

        public static IWebLocator ShadowHostDiv => L(
          "ShadowHostDiv",
          By.Id("shadow_host"));

        #endregion
    }
}
