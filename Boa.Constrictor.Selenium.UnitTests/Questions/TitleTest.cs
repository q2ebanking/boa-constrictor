﻿using Boa.Constrictor.Selenium;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.Selenium.UnitTests
{
    public class TitleTest : BaseWebQuestionTest
    {
        #region Tests

        [Test]
        public void TestGetTitle()
        {
            WebDriver.SetupGet(x => x.Title).Returns("Google Search Results");
            Actor.AsksFor(Title.OfPage()).Should().Be("Google Search Results");
        }

        #endregion
    }
}
