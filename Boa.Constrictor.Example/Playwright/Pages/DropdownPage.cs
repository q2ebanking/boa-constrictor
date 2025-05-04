using Boa.Constrictor.Playwright;

namespace Boa.Constrictor.Example;

public class DropdownPage
{
    public static string Url => "https://the-internet.herokuapp.com/dropdown";

    public static IPlaywrightLocator Dropdown => PlaywrightLocator.L("Dropdown", "#dropdown");
}