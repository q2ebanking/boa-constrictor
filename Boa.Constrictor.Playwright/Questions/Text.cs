namespace Boa.Constrictor.Playwright.Questions
{
    using System.Threading.Tasks;
    using Boa.Constrictor.Playwright.Abilities;
    using Boa.Constrictor.Screenplay;

    public class Text : IQuestionAsync<string>
    {
        private readonly string locator;

        private Text(string locator)
        {
            this.locator = locator;
        }

        public static Text Of(string locator)
        {
            return new Text(locator);
        }

        public async Task<string> RequestAsAsync(IActor actor)
        {
            var page = await actor.Using<BrowseTheWebSynchronously>().CurrentPageAsync();
            return await page.Locator(this.locator).InnerTextAsync();
        }
    }
}