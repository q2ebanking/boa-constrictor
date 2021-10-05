using Boa.Constrictor.Screenplay;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    /// <summary>
    /// These tests for the Wait task are very rudimentary.
    /// They simply verify if exceptions are thrown when questions do and do not satisfy the condition for waiting.
    /// </summary>
    [TestFixture]
    public class WaitTest
    {
        #region Test Variables

        private IActor Actor { get; set; }
        private Mock<IQuestion<int>> MockQuestionInt { get; set; }
        private Mock<ICondition<int>> MockConditionInt { get; set; }
        private Mock<IQuestion<bool>> MockQuestionBool { get; set; }
        private Mock<ICondition<bool>> MockConditionBool { get; set; }
        private Mock<IQuestion<string>> MockQuestionString { get; set; }
        private Mock<ICondition<string>> MockConditionString { get; set; }

        #endregion

        #region Setup

        [SetUp]
        public void SetUp()
        {
            Actor = new Actor();
            MockQuestionInt = new Mock<IQuestion<int>>();
            MockConditionInt = new Mock<ICondition<int>>();
            MockQuestionBool = new Mock<IQuestion<bool>>();
            MockConditionBool = new Mock<ICondition<bool>>();
            MockQuestionString = new Mock<IQuestion<string>>();
            MockConditionString = new Mock<ICondition<string>>();
        }

        #endregion

        #region Tests

        [Test]
        public void TestSuccessfulWait()
        {
            MockQuestionInt.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockConditionInt.Setup(x => x.Evaluate(It.IsAny<int>())).Returns(true);

            Actor.Invoking(actor => actor.AttemptsTo(Wait.Until(MockQuestionInt.Object, MockConditionInt.Object).ForUpTo(0)))
                .Should().NotThrow(because: "the question should satisfy the condition");
        }

        [Test]
        public void TestSuccessfulWaitAfterChange()
        {
            const int limit = 5;
            int incrementer = 0;

            MockQuestionInt.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(() => ++incrementer);
            MockConditionInt.Setup(x => x.Evaluate(It.Is<int>(v => v < limit))).Returns(false);
            MockConditionInt.Setup(x => x.Evaluate(It.Is<int>(v => v >= limit))).Returns(true);

            Actor.Invoking(actor => actor.AttemptsTo(Wait.Until(MockQuestionInt.Object, MockConditionInt.Object).ForUpTo(1)))
                .Should().NotThrow(because: "the question should satisfy the condition");

            incrementer.Should().Be(limit, because: $"the question should be called {limit} times");
        }

        [Test]
        public void TestFailedWait()
        {
            MockQuestionInt.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockConditionInt.Setup(x => x.Evaluate(It.IsAny<int>())).Returns(false);

            Actor.Invoking(actor => actor.AttemptsTo(Wait.Until(MockQuestionInt.Object, MockConditionInt.Object).ForUpTo(0)))
                .Should().Throw<WaitingException>(because: "the question should not satisfy the condition");
        }

        [Test]
        public void TestSuccessfulWaitMultipleTypesAnd()
        {
            //success
            MockQuestionInt.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockConditionInt.Setup(x => x.Evaluate(It.IsAny<int>())).Returns(true);
            //success
            MockQuestionBool.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(false);
            MockConditionBool.Setup(x => x.Evaluate(It.IsAny<bool>())).Returns(true);
            //success
            MockQuestionString.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns("title");
            MockConditionString.Setup(x => x.Evaluate(It.IsAny<string>())).Returns(true);

            Actor.Invoking(actor => actor.AttemptsTo(
                Wait.Until(MockQuestionInt.Object, MockConditionInt.Object)
                .And(MockQuestionBool.Object, MockConditionBool.Object)
                .And(MockQuestionString.Object, MockConditionString.Object)
                .ForUpTo(0)))
                .Should().NotThrow(because: "the question should satisfy the condition");
        }

        [Test]
        public void TestSuccessfulWaitMultipleTypesOr()
        {
            //fail
            MockQuestionInt.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockConditionInt.Setup(x => x.Evaluate(It.IsAny<int>())).Returns(false);
            //fail
            MockQuestionBool.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(false);
            MockConditionBool.Setup(x => x.Evaluate(It.IsAny<bool>())).Returns(false);
            //success
            MockQuestionString.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns("title");
            MockConditionString.Setup(x => x.Evaluate(It.IsAny<string>())).Returns(true);

            Actor.Invoking(actor => actor.AttemptsTo(
                Wait.Until(MockQuestionInt.Object, MockConditionInt.Object)
                .And(MockQuestionBool.Object, MockConditionBool.Object)
                .Or(MockQuestionString.Object, MockConditionString.Object)
                .ForUpTo(0)))
                .Should().NotThrow(because: "the question should satisfy the condition");
        }

        [Test]
        public void TestFailedWaitMultipleTypesAnd()
        {
            //success
            MockQuestionInt.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockConditionInt.Setup(x => x.Evaluate(It.IsAny<int>())).Returns(true);
            //fail
            MockQuestionBool.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(false);
            MockConditionBool.Setup(x => x.Evaluate(It.IsAny<bool>())).Returns(false);
            //success
            MockQuestionString.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns("title");
            MockConditionString.Setup(x => x.Evaluate(It.IsAny<string>())).Returns(true);

            Actor.Invoking(actor => actor.AttemptsTo(
                Wait.Until(MockQuestionInt.Object, MockConditionInt.Object)
                .And(MockQuestionBool.Object, MockConditionBool.Object)
                .And(MockQuestionString.Object, MockConditionString.Object)
                .ForUpTo(0)))
                .Should().Throw<WaitingException>(because: "the question should not satisfy the condition");
        }

        [Test]
        public void TestFailedWaitMultipleTypesOr()
        {
            //success
            MockQuestionInt.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockConditionInt.Setup(x => x.Evaluate(It.IsAny<int>())).Returns(true);
            //fail
            MockQuestionBool.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(false);
            MockConditionBool.Setup(x => x.Evaluate(It.IsAny<bool>())).Returns(false);
            //fail
            MockQuestionString.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns("title");
            MockConditionString.Setup(x => x.Evaluate(It.IsAny<string>())).Returns(false);

            Actor.Invoking(actor => actor.AttemptsTo(
                Wait.Until(MockQuestionInt.Object, MockConditionInt.Object)
                .And(MockQuestionBool.Object, MockConditionBool.Object)
                .Or(MockQuestionString.Object, MockConditionString.Object)
                .ForUpTo(0)))
                .Should().Throw<WaitingException>(because: "the question should not satisfy the condition");
        }

        [Test]
        public void TestSuccessfulWaitMultipleTypesAndAfterChange()
        {
            const int limit = 5;
            int incrementer = 0;

            //success
            MockQuestionInt.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(() => ++incrementer);
            MockConditionInt.Setup(x => x.Evaluate(It.Is<int>(v => v < limit))).Returns(false);
            MockConditionInt.Setup(x => x.Evaluate(It.Is<int>(v => v >= limit))).Returns(true);
            //success
            MockQuestionBool.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(false);
            MockConditionBool.Setup(x => x.Evaluate(It.IsAny<bool>())).Returns(true);
            //success
            MockQuestionString.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns("title");
            MockConditionString.Setup(x => x.Evaluate(It.IsAny<string>())).Returns(true);

            Actor.Invoking(actor => actor.AttemptsTo(
                Wait.Until(MockQuestionInt.Object, MockConditionInt.Object)
                .And(MockQuestionBool.Object, MockConditionBool.Object)
                .And(MockQuestionString.Object, MockConditionString.Object)
                .ForUpTo(1)))
                .Should().NotThrow(because: "the question should satisfy the condition");

            incrementer.Should().Be(limit, because: $"the question should be called {limit} times");
        }

        #endregion
    }
}
