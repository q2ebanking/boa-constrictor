using Boa.Constrictor.WebDriver;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

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
            var element = new Mock<IWebElement>();
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement> { element.Object }.AsReadOnly());
            WebDriver.Setup(x => x.FindElement(It.IsAny<By>())).Returns(element.Object);
            Locator = new WebLocator("description", By.Id("id"));
        }

        #endregion

        #region Methods

        protected void SetUpFindElementsReturnsEmpty()
        {
            WebDriver.Setup(x => x.FindElements(It.IsAny<By>())).Returns(new List<IWebElement>().AsReadOnly());
        }

        #endregion
    }
}
