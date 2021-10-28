using Boa.Constrictor.WebDriver;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class WindowHandleTest : BaseWebQuestionTest
    {
        #region Test Variables

        private ReadOnlyCollection<string> WindowHandles;

        #endregion 

        #region SetUp

        [SetUp]
        public void SetUp()
        {
            WindowHandles = new List<string>
            {
                "5789283747",
                "3785873245",
                "7983578344"
            }.AsReadOnly();

            WebDriver.SetupGet(x => x.WindowHandles).Returns(WindowHandles);
        }

        #endregion

        #region Tests

        [Test]
        public void CurrentHandle()
        {
            WebDriver.SetupGet(x => x.CurrentWindowHandle).Returns(WindowHandles[0]);

            Actor.AsksFor(WindowHandle.Current()).Should().Be(WindowHandles[0]);
        }

        [Test]
        public void LastHandle()
        {
            Actor.AsksFor(WindowHandle.Last()).Should().Be(WindowHandles.Last());
        }

        [Test]
        public void HandleAtIndex()
        {
            Actor.AsksFor(WindowHandle.At(1)).Should().Be(WindowHandles[1]);
        }

        [Test]
        public void HandleIndexOutOfBounds()
        {
            Actor.Invoking(x => x.AsksFor(WindowHandle.At(5)))
                .Should().Throw<BrowserInteractionException>()
                .WithMessage("No browser window exists at index '5'");
        }

        [Test]
        public void LatestHandle()
        {
            Actor.AsksFor(WindowHandle.Latest()).Should().Be(WindowHandles.First());
        }

        #endregion
    }
}
