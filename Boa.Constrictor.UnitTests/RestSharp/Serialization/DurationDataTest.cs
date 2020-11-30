using Boa.Constrictor.RestSharp;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Boa.Constrictor.UnitTests.RestSharp
{
    [TestFixture]
    public class DurationDataTest
    {
        [Test]
        public void Init()
        {
            var start = DateTime.UtcNow;
            var end = DateTime.Now;
            var data = new DurationData(start, end);
            data.StartTime.Should().Be(start);
            data.EndTime.Should().Be(end);
        }

        [Test]
        public void InitStartTimeNull()
        {
            var end = DateTime.Now;
            var data = new DurationData(null, end);
            data.StartTime.Should().BeNull();
            data.EndTime.Should().Be(end);
        }

        [Test]
        public void InitEndTimeNull()
        {
            var start = DateTime.UtcNow;
            var data = new DurationData(start, null);
            data.StartTime.Should().Be(start);
            data.EndTime.Should().BeNull();
        }

        [Test]
        public void Duration()
        {
            var start = DateTime.UtcNow;
            var end = DateTime.Now;
            var data = new DurationData(start, end);
            data.Duration.Should().Be(end - start);
        }

        [Test]
        public void DurationStartTimeNull()
        {
            var end = DateTime.UtcNow;
            var data = new DurationData(null, end);
            data.Duration.Should().BeNull();
        }

        [Test]
        public void DurationEndTimeNull()
        {
            var start = DateTime.UtcNow;
            var data = new DurationData(start, null);
            data.Duration.Should().BeNull();
        }

        [Test]
        public void DurationBothNull()
        {
            var data = new DurationData(null, null);
            data.Duration.Should().BeNull();
        }
    }
}
