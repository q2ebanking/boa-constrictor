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

        private IActor Screenplayer { get; set; }
        private Mock<IQuestion<int>> MockQuestion { get; set; }
        private Mock<ICondition<int>> MockCondition { get; set; }

        #endregion

        #region Setup

        [SetUp]
        public void SetUp()
        {
            Screenplayer = new Actor();
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

            Screenplayer.Invoking(actor => actor.AttemptsTo(Wait.Until(MockQuestion.Object, MockCondition.Object).ForUpTo(0)))
                .Should().NotThrow(because: "the question should satisfy the condition");
        }

        [Test]
        public void TestSuccessfulWaitAfterChange()
        {
            const int limit = 5;
            int incrementer = 0;

            MockQuestion.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(() => ++incrementer);
            MockCondition.Setup(x => x.Evaluate(It.Is<int>(v => v < limit))).Returns(false);
            MockCondition.Setup(x => x.Evaluate(It.Is<int>(v => v >= limit))).Returns(true);

            Screenplayer.Invoking(actor => actor.AttemptsTo(Wait.Until(MockQuestion.Object, MockCondition.Object).ForUpTo(1)))
                .Should().NotThrow(because: "the question should satisfy the condition");

            incrementer.Should().Be(limit, because: $"the question should be called {limit} times");
        }

        [Test]
        public void TestFailedWait()
        {
            MockQuestion.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockCondition.Setup(x => x.Evaluate(It.IsAny<int>())).Returns(false);

            Screenplayer.Invoking(actor => actor.AttemptsTo(Wait.Until(MockQuestion.Object, MockCondition.Object).ForUpTo(0)))
                .Should().Throw<WaitingException<int>>(because: "the question should not satisfy the condition");
        }

        #endregion
    }
}
