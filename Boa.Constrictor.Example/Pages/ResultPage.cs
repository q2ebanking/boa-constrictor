using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Boa.Constrictor.Example
{
    public static class ResultPage
    {
        public static IWebLocator ResultLinks => L(
          "DuckDuckGo Result Page Links",
          By.ClassName("result__a"));
    }
}
