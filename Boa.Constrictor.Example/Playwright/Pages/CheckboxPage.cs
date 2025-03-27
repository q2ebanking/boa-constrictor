using Boa.Constrictor.Playwright;

namespace Boa.Constrictor.Example;

public class CheckboxPage
{
    public static string Url => "https://the-internet.herokuapp.com/checkboxes";

    public static IPlaywrightLocator Checkbox1 => PlaywrightLocator.L("Checkbox 1", page => page.Locator("input").Locator("nth=0"));
    
    public static IPlaywrightLocator Checkbox2 => PlaywrightLocator.L("Checkbox 2", page => page.Locator("input").Locator("nth=1"));
}