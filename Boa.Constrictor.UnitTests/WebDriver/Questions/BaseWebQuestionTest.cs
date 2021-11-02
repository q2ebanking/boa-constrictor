using Boa.Constrictor.Logging;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    [TestFixture]
    public class BaseWebQuestionTest
    {
        #region Test Variables

        public IActor Actor { get; set; }
        public Mock<ITestWebDriver> WebDriver { get; set; }
        public ListLogger Logger => (ListLogger)Actor.Logger;

        #endregion

        #region SetUp

        [SetUp]
        public void SetUpActor()
        {
            Actor = new Actor(logger:new ListLogger());
            WebDriver = new Mock<ITestWebDriver>();
            WebDriver.SetupGet(x => x.WindowHandles).Returns(new List<string>().AsReadOnly());
            Actor.Can(BrowseTheWeb.With(WebDriver.Object));
            Actor.Can(SetTimeouts.To(0));
        }

        #endregion
    }
}
