using Boa.Constrictor.Selenium;
using OpenQA.Selenium;
using static Boa.Constrictor.Selenium.WebLocator;

namespace Boa.Constrictor.Example
{
    public static class SearchPage
    {
        public const string Url = "https://www.duckduckgo.com/";

        public static IWebLocator SearchButton => L(
          "DuckDuckGo Search Button",
          By.Id("search_button_homepage"));

        public static IWebLocator SearchInput => L(
          "DuckDuckGo Search Input",
          By.Id("search_form_input_homepage"));
    }
}
