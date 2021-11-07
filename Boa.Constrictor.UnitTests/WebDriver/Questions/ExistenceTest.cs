using Boa.Constrictor.WebDriver;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    public class ExistenceTest : BaseWebLocatorQuestionTest
    {
        #region Tests

        [Test]
        public void TestElementExists()
        {
            Actor.AsksFor(Existence.Of(Locator)).Should().BeTrue();
        }

        [Test]
        public void TestElementDoesNotExist()
        {
            SetUpFindElementsReturnsEmpty();

            Actor.AsksFor(Existence.Of(Locator)).Should().BeFalse();
        }

        #endregion
    }
}
