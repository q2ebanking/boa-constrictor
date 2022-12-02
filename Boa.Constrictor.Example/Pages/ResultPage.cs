using Boa.Constrictor.Selenium;
using OpenQA.Selenium;
using static Boa.Constrictor.Selenium.WebLocator;

namespace Boa.Constrictor.Example
{
    public static class ResultPage
    {
        public static IWebLocator ResultLinks => L(
          "DuckDuckGo Result Page Links",
          By.ClassName("result__a"));
    }
}
