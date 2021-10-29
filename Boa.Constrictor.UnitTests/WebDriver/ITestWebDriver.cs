using OpenQA.Selenium;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public interface ITestWebDriver : IWebDriver, ITakesScreenshot, IJavaScriptExecutor { }
}
