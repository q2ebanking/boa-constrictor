using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class DomPropertyTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestDomProperty()
        {
            WebDriver.Setup(x => x.FindElement(It.IsAny<By>()).GetDomProperty(It.IsAny<string>())).Returns("blank");

            Actor.AsksFor(DomProperty.Of(Locator, "target")).Should().Be("blank");
        }

        [Test]
        public void TestZeroElements()
        {
            SetUpFindElementsReturnsEmpty();

            Actor.Invoking(x => x.AsksFor(DomProperty.Of(Locator, "target"))).Should().Throw<WaitingException<bool>>();
        }

        #endregion
    }
}
