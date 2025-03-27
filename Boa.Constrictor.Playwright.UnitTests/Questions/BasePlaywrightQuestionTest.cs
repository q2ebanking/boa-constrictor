using Boa.Constrictor.Screenplay;
using Microsoft.Playwright;
using Moq;
using NUnit.Framework;

namespace Boa.Constrictor.Playwright.UnitTests.Questions;

[TestFixture]
public class BasePlaywrightQuestionTest
{
    #region Test Variables
    
    public IActor Actor { get; set; }
    
    public Mock<IPlaywright> Playwright { get; set; }
    
    public Mock<IBrowser> Browser { get; set; }
    
    public Mock<IPage> Page { get; set; }
    
    #endregion
    
    #region Setup

    [SetUp]
    public void SetupActor()
    {
        Playwright = new Mock<IPlaywright>();
        
        Browser = new Mock<IBrowser>();
        Browser.Setup(x => x.BrowserType.Name).Returns("Firefox");
        
        Page = new Mock<IPage>();
        
        Actor = new Actor(logger: new ListLogger());
        var browseTheWeb = BrowseTheWebWithPlaywright.Using(Playwright.Object, Browser.Object);
        browseTheWeb.CurrentPage = Page.Object;
        Actor.Can(browseTheWeb);
    }

    #endregion
}