using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Selenium;

namespace Boa.Constrictor.Example
{
    public class SearchWikipedia : ITask
    {
        public string Phrase { get; }

        private SearchWikipedia(string phrase) =>
            Phrase = phrase;

        public static SearchWikipedia For(string phrase) =>
            new SearchWikipedia(phrase);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(SendKeys.To(MainPage.SearchInput, Phrase));
            actor.AttemptsTo(Click.On(MainPage.SearchButton));
        }
    }
}