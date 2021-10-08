using Boa.Constrictor.Screenplay;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    /// <summary>
    /// These tests for the ValueAfterWaiting Question are very rudimentary.
    /// They simply verify if exceptions are thrown when Questions do and do not satisfy the condition for waiting.
    /// </summary>
    [TestFixture]
    public class ValueAfterWaitingTest
    {
        #region Test Variables

        private IActor Actor { get; set; }
        private Mock<IQuestion<int>> MockQuestion { get; set; }
        private Mock<ICondition<int>> MockCondition { get; set; }

        #endregion

        #region Setup

        [SetUp]
        public void SetUp()
        {
            Actor = new Actor();
            MockQuestion = new Mock<IQuestion<int>>();
            MockCondition = new Mock<ICondition<int>>();
        }

        #endregion

        #region Tests

        [Test]
        public void TestSuccessfulWait()
        {
            MockQuestion.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockCondition.Setup(x => x.Evaluate(It.IsAny<int>())).Returns(true);

            Actor.AskingFor(ValueAfterWaiting.Until(MockQuestion.Object, MockCondition.Object).ForUpTo(0))
                .Should().Be(1, because: "waiting should return the Question's final answer");
        }

        [Test]
        public void TestSuccessfulWaitAfterChange()
        {
            const int limit = 5;
            int incrementer = 0;

            MockQuestion.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(() => ++incrementer);
            MockCondition.Setup(x => x.Evaluate(It.Is<int>(v => v < limit))).Returns(false);
            MockCondition.Setup(x => x.Evaluate(It.Is<int>(v => v >= limit))).Returns(true);

            Actor.AskingFor(ValueAfterWaiting.Until(MockQuestion.Object, MockCondition.Object).ForUpTo(1))
                .Should().Be(limit, because: "waiting should return the Question's final answer");

            incrementer.Should().Be(limit, because: $"the Question should be called {limit} times");
        }

        [Test]
        public void TestFailedWait()
        {
            MockQuestion.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockCondition.Setup(x => x.Evaluate(It.IsAny<int>())).Returns(false);

            Actor.Invoking(actor => actor.AskingFor(ValueAfterWaiting.Until(MockQuestion.Object, MockCondition.Object).ForUpTo(0)))
                .Should().Throw<WaitingException<int>>(because: "the Question should not satisfy the condition");
        }

        #endregion
    }
}
