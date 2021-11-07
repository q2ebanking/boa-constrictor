using Boa.Constrictor.WebDriver;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class BaseWebLocatorQuestionTest : BaseWebQuestionTest
    {
        #region Test Variables

        public IWebLocator Locator;

        #endregion

        #region SetUp

        [SetUp]
        public void SetUpLocator()
        {
            Locator = new WebLocator("description", By.Id("id"));
        }

        #endregion
    }
}
