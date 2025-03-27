using Boa.Constrictor.Playwright;

namespace Boa.Constrictor.Example;

public class LoginPage
{
    public static string Url => "https://the-internet.herokuapp.com/login";

    public static IPlaywrightLocator UsernameInput => PlaywrightLocator.L("Username Input", "#username");
    
    public static IPlaywrightLocator PasswordInput => PlaywrightLocator.L("Password Input", "#password");
    
    public static IPlaywrightLocator LoginButton => PlaywrightLocator.L("Login Button", "button");
}