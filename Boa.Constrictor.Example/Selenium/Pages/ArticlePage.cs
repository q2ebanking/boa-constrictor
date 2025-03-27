using Boa.Constrictor.Selenium;
using OpenQA.Selenium;
using static Boa.Constrictor.Selenium.WebLocator;

namespace Boa.Constrictor.Example
{
    public static class ArticlePage
    {
        public static IWebLocator Title => L(
          "Title Span",
          By.CssSelector("[id='firstHeading'] span"));
    }
}
