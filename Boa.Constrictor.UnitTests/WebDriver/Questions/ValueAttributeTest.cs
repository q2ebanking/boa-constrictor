using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class ValueAttributeTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestAttribute()
        {
            WebDriver.Setup(x => x.FindElement(It.IsAny<By>()).GetAttribute(It.IsAny<string>())).Returns("a");

            Actor.AsksFor(ValueAttribute.Of(Locator)).Should().Be("a");
        }

        [Test]
        public void TestZeroElements()
        {
            SetUpFindElementsReturnsEmpty();

            Actor.Invoking(x => x.AsksFor(ValueAttribute.Of(Locator))).Should().Throw<WaitingException<bool>>();
        }

        #endregion
    }
}
