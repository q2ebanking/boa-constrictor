using Boa.Constrictor.Playwright;
using OpenQA.Selenium;

namespace Boa.Constrictor.Example
{
    public static class ArticlePage
    {
        public static BoaWebLocator Title => BoaWebLocator.L(
          "Title Span",
          By.CssSelector("[id='firstHeading'] span"));
    }
}
