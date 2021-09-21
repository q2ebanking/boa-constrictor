using Boa.Constrictor.Safety;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace Boa.Constrictor.UnitTests.Safety
{
    [TestFixture]
    public class SafeActionsExceptionTest
    {
        #region Constants

        private static readonly Exception ExA = new Exception("A");
        private static readonly Exception ExB = new Exception("B");
        private static readonly Exception ExC = new Exception("C");

        #endregion

        #region Tests

        [Test]
        public void Init_None()
        {
            var ex = new SafeActionsException();
            ex.Message.Should().Be("(No failures provided)");
            ex.Failures.Should().BeEmpty();
        }

        [Test]
        public void Init_One()
        {
            var ex = new SafeActionsException(ExA);
            ex.Message.Should().Be($"(1) {ExA.Message}");

            var failures = ex.Failures.ToArray();
            failures.Should().HaveCount(1);
            failures[0].Should().BeSameAs(ExA);
        }

        [Test]
        public void Init_ManyParams()
        {
            var ex = new SafeActionsException(ExA, ExB, ExC);
            ex.Message.Should().Be($"(1) {ExA.Message}; (2) {ExB.Message}; (3) {ExC.Message}");

            var failures = ex.Failures.ToArray();
            failures.Should().HaveCount(3);
            failures[0].Should().BeSameAs(ExA);
            failures[1].Should().BeSameAs(ExB);
            failures[2].Should().BeSameAs(ExC);
        }

        [Test]
        public void Init_ManyEnumerable()
        {
            var exceptions = new Exception[] { ExA, ExB, ExC };
            var ex = new SafeActionsException(exceptions);
            ex.Message.Should().Be($"(1) {ExA.Message}; (2) {ExB.Message}; (3) {ExC.Message}");

            var failures = ex.Failures.ToArray();
            failures.Should().HaveCount(3);
            failures[0].Should().BeSameAs(ExA);
            failures[1].Should().BeSameAs(ExB);
            failures[2].Should().BeSameAs(ExC);
        }

        #endregion
    }
}
