using Boa.Constrictor.Safety;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Boa.Constrictor.UnitTests.Safety
{
    [TestFixture]
    public class SafeActionsTest
    {
        #region Boolean Holder Class

        public class BooleanHolder
        {
            public bool Value { get; set; }
        }

        #endregion

        #region Action Builders

        public static Action[] PassingActions(BooleanHolder tested)
        {
            return new Action[]
            {
                () => 1.Should().Be(1),
                () => 2.Should().Be(2),
                () => { tested.Value = true; 3.Should().Be(3); }
            };
        }

        public static Action[] OneFailureActions(BooleanHolder tested)
        {
            return new Action[]
            {
                () => 1.Should().Be(1),
                () => 2.Should().Be(0),
                () => { tested.Value = true; 3.Should().Be(3); }
            };
        }

        public static Action[] ManyFailureActions(BooleanHolder tested)
        {
            return new Action[]
            {
                () => 1.Should().Be(0),
                () => 2.Should().Be(0),
                () => { tested.Value = true; 3.Should().Be(3); }
            };
        }

        #endregion

        #region Failure Message Constants

        public const string Message_OneFailure = "(1) Expected * to be 0, but found 2.";
        public const string Message_ManyFailures = "(1) Expected * to be 0, but found 1.; (2) Expected * to be 0, but found 2.";

        #endregion

        #region Tests for Static Methods

        public class StaticMethodTest
        {
            // The tests here should use the params version of the SoftlyAssert method.
            // It implicitly tests the IEnumerable<Action> version.

            private BooleanHolder Tested;

            [SetUp]
            public void SetUp()
            {
                Tested = new BooleanHolder() { Value = false };
            }

            [Test]
            public void SoftlyAssert_Pass()
            {
                Action sa = () => SafeActions.Safely(PassingActions(Tested));
                sa.Should().NotThrow();
                Tested.Value.Should().BeTrue();
            }

            [Test]
            public void SoftlyAssert_OneFailure()
            {
                Action sa = () => SafeActions.Safely(OneFailureActions(Tested));
                sa.Should().Throw<SafeActionsException>().WithMessage(Message_OneFailure);
                Tested.Value.Should().BeTrue();
            }

            [Test]
            public void SoftlyAssert_ManyFailures()
            {
                Action sa = () => SafeActions.Safely(ManyFailureActions(Tested));
                sa.Should().Throw<SafeActionsException>().WithMessage(Message_ManyFailures);
                Tested.Value.Should().BeTrue();
            }
        }

        #endregion

        #region Tests for Class

        public class ClassTest
        {
            private SafeActions Actions;
            private BooleanHolder Tested;

            [SetUp]
            public void SetUp()
            {
                Actions = new SafeActions();
                Tested = new BooleanHolder() { Value = false };
            }

            [Test]
            public void Emtpy()
            {
                Actions.Invoking(a => a.ThrowAnyFailures()).Should().NotThrow();
            }
            
            [Test]
            public void AttemptAndThrow_Pass()
            {
                Actions.Invoking(a => a.Attempt(PassingActions(Tested))).Should().NotThrow();
                Actions.Invoking(a => a.ThrowAnyFailures()).Should().NotThrow();
                Tested.Value.Should().BeTrue();
            }

            [Test]
            public void AttemptAndThrow_OneFailure()
            {
                Actions.Invoking(a => a.Attempt(OneFailureActions(Tested))).Should().NotThrow();
                Actions.Invoking(a => a.ThrowAnyFailures()).Should().Throw<SafeActionsException>().WithMessage(Message_OneFailure);
                Tested.Value.Should().BeTrue();
            }

            [Test]
            public void AttemptAndThrow_ManyFailures()
            {
                Actions.Invoking(a => a.Attempt(ManyFailureActions(Tested))).Should().NotThrow();
                Actions.Invoking(a => a.ThrowAnyFailures()).Should().Throw<SafeActionsException>().WithMessage(Message_ManyFailures);
                Tested.Value.Should().BeTrue();
            }

            [Test]
            public void FailureHandler_Skipped()
            {
                BooleanHolder handlerHolder = new BooleanHolder() { Value = false };
                void failureHandler(Exception e) => handlerHolder.Value = true;
                Actions = new SafeActions(failureHandler);

                Actions.Attempt(PassingActions(Tested));
                handlerHolder.Value.Should().BeFalse();
            }

            [Test]
            public void FailureHandler_Executed()
            {
                BooleanHolder handlerHolder = new BooleanHolder() { Value = false };
                void failureHandler(Exception e) => handlerHolder.Value = true;
                Actions = new SafeActions(failureHandler);

                Actions.Attempt(OneFailureActions(Tested));
                handlerHolder.Value.Should().BeTrue();
            }
        }

        #endregion
    }
}
