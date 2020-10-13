using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;

namespace Boa.Constrictor.Example
{
    public class SearchDuckDuckGo : ITask
    {
        public string Phrase { get; }

        private SearchDuckDuckGo(string phrase) =>
          Phrase = phrase;

        public static SearchDuckDuckGo For(string phrase) =>
          new SearchDuckDuckGo(phrase);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(SendKeys.To(SearchPage.SearchInput, Phrase));
            actor.AttemptsTo(Click.On(SearchPage.SearchButton));
        }
    }
}