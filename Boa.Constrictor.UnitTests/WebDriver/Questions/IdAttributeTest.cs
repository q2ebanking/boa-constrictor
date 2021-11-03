using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class IdAttributeTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestAttribute()
        {
            WebDriver.Setup(x => x.FindElement(It.IsAny<By>()).GetAttribute(It.IsAny<string>())).Returns("a_button");

            Actor.AsksFor(IdAttribute.Of(Locator)).Should().Be("a_button");
        }

        [Test]
        public void TestZeroElements()
        {
            SetUpFindElementsReturnsEmpty();

            Actor.Invoking(x => x.AsksFor(IdAttribute.Of(Locator))).Should().Throw<WaitingException<bool>>();
        }

        #endregion
    }
}
