using OpenQA.Selenium;

namespace Boa.Constrictor.Selenium.UnitTests
{
    public interface ITestWebDriver : IWebDriver, ITakesScreenshot, IJavaScriptExecutor { }
}
