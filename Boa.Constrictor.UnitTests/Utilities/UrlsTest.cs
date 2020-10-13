using Boa.Constrictor.Utilities;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Boa.Constrictor.UnitTests.Utilities
{
    [TestFixture]
    public class UrlsTest
    {
        #region Tests

        [TestCase("https://www.base.com", "relative")]
        [TestCase("https://www.base.com/", "relative")]
        [TestCase("https://www.base.com", "/relative")]
        [TestCase("https://www.base.com/", "/relative")]
        [TestCase("https://www.base.com//", "/relative")]
        [TestCase("    https://www.base.com    ", "    relative    ")]
        public void Combine(string baseUrl, string relativeUrl)
        {
            Urls.Combine(baseUrl, relativeUrl).Should().Be("https://www.base.com/relative");
        }

        [TestCase("https://www.base.com", "")]
        [TestCase("https://www.base.com/", "")]
        [TestCase("https://www.base.com/", "    ")]
        [TestCase("https://www.base.com/", null)]
        public void Combine_NoRelative(string baseUrl, string relativeUrl)
        {
            Urls.Combine(baseUrl, relativeUrl).Should().Be("https://www.base.com/");
        }

        [TestCase("www.base.com", "relative")]
        [TestCase(null, "relative")]
        public void Combine_Exception(string baseUrl, string relativeUrl)
        {
            Action combine = () => Urls.Combine(baseUrl, relativeUrl);
            combine.Should().Throw<Exception>();
        }

        #endregion
    }
}
