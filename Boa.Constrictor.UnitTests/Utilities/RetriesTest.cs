using Boa.Constrictor.Utilities;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Boa.Constrictor.UnitTests.Utilities
{
    public class RetryException : Exception { }

    [TestFixture]
    public class RetriesTest
    {
        #region Tests
        
        [Test]
        public void RetryOnException_Okay()
        {
            int attempts = 0;
            bool call() { attempts++; return true; }
            Retries.RetryOnException<RetryException, bool>(call, "test call").Should().BeTrue();
            attempts.Should().Be(1);
        }

        [Test]
        public void RetryOnException_OneException()
        {
            int attempts = 0;
            bool call() { if (attempts++ == 0) throw new RetryException(); return true; }
            Retries.RetryOnException<RetryException, bool>(call, "test call").Should().BeTrue();
            attempts.Should().Be(2);
        }
        
        [Test]
        public void RetryOnException_RepeatedException()
        {
            int attempts = 0;
            bool call() { attempts++; throw new RetryException(); }
            Action callingRetry = () => Retries.RetryOnException<RetryException, bool>(call, "test call");
            callingRetry.Should().Throw<RetryException>();
            attempts.Should().Be(3);
        }
        
        [Test]
        public void RetryOnException_ImmediateAbort()
        {
            int attempts = 0;
            bool call() { attempts++; throw new Exception(); }
            Action callingRetry = () => Retries.RetryOnException<RetryException, bool>(call, "test call");
            callingRetry.Should().Throw<Exception>();
            attempts.Should().Be(1);
        }
        
        #endregion
    }
}
