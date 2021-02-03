using Boa.Constrictor.WebDriver;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    [TestFixture]
    public class WebLocatorTest
    {
        [Test]
        public void Equals_True()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new WebLocator("hello", By.Id("moto"));
            a.Equals(b).Should().BeTrue();
        }

        [Test]
        public void Equals_False_NonWebLocator()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new object();
            a.Equals(b).Should().BeFalse();
        }

        [Test]
        public void Equals_False_Description()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new WebLocator("goodbye", By.Id("moto"));
            a.Equals(b).Should().BeFalse();
        }

        [Test]
        public void Equals_False_QueryValue()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new WebLocator("hello", By.Id("goodbye"));
            a.Equals(b).Should().BeFalse();
        }

        [Test]
        public void Equals_False_QueryType()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new WebLocator("hello", By.CssSelector("moto"));
            a.Equals(b).Should().BeFalse();
        }

        [Test]
        public void GetHashCode_Same()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new WebLocator("hello", By.Id("moto"));
            int codeA = a.GetHashCode();
            int codeB = b.GetHashCode();
            codeA.Should().Be(codeB);
        }

        [Test]
        public void GetHashCode_Different_NonWebLocator()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new object();
            int codeA = a.GetHashCode();
            int codeB = b.GetHashCode();
            codeA.Should().NotBe(codeB);
        }

        [Test]
        public void GetHashCode_Different_Description()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new WebLocator("goodbye", By.Id("moto"));
            int codeA = a.GetHashCode();
            int codeB = b.GetHashCode();
            codeA.Should().NotBe(codeB);
        }

        [Test]
        public void GetHashCode_Different_QueryValue()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new WebLocator("hello", By.Id("goodbye"));
            int codeA = a.GetHashCode();
            int codeB = b.GetHashCode();
            codeA.Should().NotBe(codeB);
        }

        [Test]
        public void GetHashCode_Different_QueryType()
        {
            var a = new WebLocator("hello", By.Id("moto"));
            var b = new WebLocator("hello", By.CssSelector("moto"));
            int codeA = a.GetHashCode();
            int codeB = b.GetHashCode();
            codeA.Should().NotBe(codeB);
        }
    }
}
