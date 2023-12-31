namespace Boa.Constrictor.Playwright.Questions
{
    using System.Threading.Tasks;
    using Boa.Constrictor.Playwright.Abilities;
    using Boa.Constrictor.Screenplay;
    using Microsoft.Playwright;

    public class Attribute : IQuestionAsync<string>
    {
        private readonly string Selector;
        private readonly string Name;
        private readonly PageGetAttributeOptions Options;

        private Attribute(string selector, string name, PageGetAttributeOptions options)
        {
            Selector = selector;
            Name = name;
            Options = options;
        }

        public static Attribute Of(string selector, string name, PageGetAttributeOptions options = null)
        {
            return new Attribute(selector, name, options);
        }
        public async Task<string> RequestAsAsync(IActor actor)
        {
            var page = await actor.Using<BrowseTheWebSynchronously>().CurrentPageAsync();
            return await page.GetAttributeAsync(Selector, Name, Options);
        }
    }
}