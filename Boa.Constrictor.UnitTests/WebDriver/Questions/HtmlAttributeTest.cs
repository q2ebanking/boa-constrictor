using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class HtmlAttributeTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestAttribute()
        {
            WebDriver.Setup(x => x.FindElement(It.IsAny<By>()).GetAttribute(It.IsAny<string>())).Returns("text");

            Actor.AsksFor(HtmlAttribute.Of(Locator, "type")).Should().Be("text");
        }

        [Test]
        public void TestZeroElements()
        {
            SetUpFindElementsReturnsEmpty();

            Actor.Invoking(x => x.AsksFor(HtmlAttribute.Of(Locator, "type"))).Should().Throw<WaitingException<bool>>();
        }

        #endregion
    }
}
