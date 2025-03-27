using Microsoft.Playwright;
using Moq;
using NUnit.Framework;

namespace Boa.Constrictor.Playwright.UnitTests.Questions;

public class BasePlaywrightLocatorQuestionTest : BasePlaywrightQuestionTest
{
    #region Test Variables
    
    public Mock<IPlaywrightLocator> PlaywrightLocator { get; set; }
    
    public Mock<ILocator> Locator { get; set; }
    
    #endregion
    
    #region Setup

    [SetUp]
    public void SetupLocator()
    {
        Locator = new Mock<ILocator>();
        PlaywrightLocator = new Mock<IPlaywrightLocator>();
        PlaywrightLocator.Setup(x => x.FindIn(Page.Object)).Returns(Locator.Object);
    }
    #endregion
}