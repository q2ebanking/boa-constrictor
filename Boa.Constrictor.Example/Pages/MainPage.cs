using Boa.Constrictor.Selenium;
using OpenQA.Selenium;
using static Boa.Constrictor.Selenium.WebLocator;

namespace Boa.Constrictor.Example
{
    public static class MainPage
    {
        public const string Url = "https://en.wikipedia.org/wiki/Main_Page";

        public static IWebLocator SearchButton => L(
          "Wikipedia Search Button",
          By.XPath("//button[text()='Search']"));

        public static IWebLocator SearchInput => L(
          "Wikipedia Search Input",
          By.Name("search"));
    }
}
