using Boa.Constrictor.Playwright.Elements;
using OpenQA.Selenium;
namespace Boa.Constrictor.Example
{
    public static class MainPage
    {
        public const string Url = "https://en.wikipedia.org/wiki/Main_Page";

        public static BoaWebLocator SearchButton => BoaWebLocator.L(
          "Wikipedia Search Button",
          By.XPath("//button[text()='Search']"));

        public static BoaWebLocator SearchInput => BoaWebLocator.L(
          "Wikipedia Search Input",
          By.Name("search"));

    }
}
