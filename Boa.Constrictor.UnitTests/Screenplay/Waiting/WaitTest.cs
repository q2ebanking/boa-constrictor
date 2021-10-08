using Boa.Constrictor.Screenplay;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    /// <summary>
    /// These tests for the Wait Task are very rudimentary.
    /// They simply verify if exceptions are thrown when Questions do and do not satisfy the condition for waiting.
    /// </summary>
    [TestFixture]
    public class WaitTest
    {
        #region Test Variables

        private IActor Actor { get; set; }
        private Mock<IQuestion<int>> MockQuestionA { get; set; }
        private Mock<ICondition<int>> MockConditionA { get; set; }
        private Mock<IQuestion<bool>> MockQuestionB { get; set; }
        private Mock<ICondition<bool>> MockConditionB { get; set; }
        private Mock<IQuestion<string>> MockQuestionC { get; set; }
        private Mock<ICondition<string>> MockConditionC { get; set; }
        private Mock<IQuestion<int?>> MockQuestionD { get; set; }
        private Mock<ICondition<int?>> MockConditionD { get; set; }

        #endregion

        #region Setup

        [SetUp]
        public void SetUp()
        {
            Actor = new Actor();
            MockQuestionA = new Mock<IQuestion<int>>();
            MockConditionA = new Mock<ICondition<int>>();
            MockQuestionB = new Mock<IQuestion<bool>>();
            MockConditionB = new Mock<ICondition<bool>>();
            MockQuestionC = new Mock<IQuestion<string>>();
            MockConditionC = new Mock<ICondition<string>>();
            MockQuestionD = new Mock<IQuestion<int?>>();
            MockConditionD = new Mock<ICondition<int?>>();
        }

        #endregion

        #region Tests

        [TestCase(true, ExpectedResult = true)]
        [TestCase(false, ExpectedResult = false)]
        public bool TestSuccessfulWait(bool a)
        {
            MockQuestionA.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockConditionA.Setup(x => x.Evaluate(It.IsAny<int>())).Returns(a);

            bool waitWithoutException = true;

            try
            {
                Actor.AttemptsTo(
                    Wait.Until(MockQuestionA.Object, MockConditionA.Object)
                    .ForUpTo(0));
            }
            catch (WaitingException)
            {
                waitWithoutException = false;
            }

            return waitWithoutException;
        }

        [TestCase(true, true, ExpectedResult = true)]
        [TestCase(true, false, ExpectedResult = false)]
        [TestCase(false, true, ExpectedResult = false)]
        [TestCase(false, false, ExpectedResult = false)]
        public bool TestWaitAnd(bool a, bool b)
        {
            MockQuestionA.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockConditionA.Setup(x => x.Evaluate(It.IsAny<int>())).Returns(a);
            MockQuestionB.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(false);
            MockConditionB.Setup(x => x.Evaluate(It.IsAny<bool>())).Returns(b);

            bool waitWithoutException = true;

            try
            {
                Actor.AttemptsTo(
                    Wait.Until(MockQuestionA.Object, MockConditionA.Object)
                    .And(MockQuestionB.Object, MockConditionB.Object)
                    .ForUpTo(0));
            }
            catch (WaitingException)
            {
                waitWithoutException = false;
            }

            return waitWithoutException;
        }

        [TestCase(true, true, ExpectedResult = true)]
        [TestCase(true, false, ExpectedResult = true)]
        [TestCase(false, true, ExpectedResult = true)]
        [TestCase(false, false, ExpectedResult = false)]
        public bool TestWaitOr(bool a, bool b)
        {
            MockQuestionA.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockConditionA.Setup(x => x.Evaluate(It.IsAny<int>())).Returns(a);
            MockQuestionB.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(false);
            MockConditionB.Setup(x => x.Evaluate(It.IsAny<bool>())).Returns(b);

            bool waitWithoutException = true;

            try
            {
                Actor.AttemptsTo(
                    Wait.Until(MockQuestionA.Object, MockConditionA.Object)
                    .Or(MockQuestionB.Object, MockConditionB.Object)
                    .ForUpTo(0));
            }
            catch (WaitingException)
            {
                waitWithoutException = false;
            }

            return waitWithoutException;
        }

        [TestCase(true, true, true, ExpectedResult = true)]
        [TestCase(true, true, false, ExpectedResult = false)]
        [TestCase(true, false, true, ExpectedResult = false)]
        [TestCase(false, true, true, ExpectedResult = false)]
        [TestCase(true, false, false, ExpectedResult = false)]
        [TestCase(false, true, false, ExpectedResult = false)]
        [TestCase(false, false, true, ExpectedResult = false)]
        [TestCase(false, false, false, ExpectedResult = false)]
        public bool TestWaitAndAnd(bool a, bool b, bool c)
        {
            MockQuestionA.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockConditionA.Setup(x => x.Evaluate(It.IsAny<int>())).Returns(a);
            MockQuestionB.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(false);
            MockConditionB.Setup(x => x.Evaluate(It.IsAny<bool>())).Returns(b);
            MockQuestionC.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns("title");
            MockConditionC.Setup(x => x.Evaluate(It.IsAny<string>())).Returns(c);

            bool waitWithoutException = true;

            try
            {
                Actor.AttemptsTo(
                    Wait.Until(MockQuestionA.Object, MockConditionA.Object)
                    .And(MockQuestionB.Object, MockConditionB.Object)
                    .And(MockQuestionC.Object, MockConditionC.Object)
                    .ForUpTo(0));
            }
            catch (WaitingException)
            {
                waitWithoutException = false;
            }

            return waitWithoutException;
        }

        [TestCase(true, true, true, ExpectedResult = true)]
        [TestCase(true, true, false, ExpectedResult = true)]
        [TestCase(true, false, true, ExpectedResult = true)]
        [TestCase(false, true, true, ExpectedResult = true)]
        [TestCase(true, false, false, ExpectedResult = false)]
        [TestCase(false, true, false, ExpectedResult = false)]
        [TestCase(false, false, true, ExpectedResult = true)]
        [TestCase(false, false, false, ExpectedResult = false)]
        public bool TestWaitAndOr(bool a, bool b, bool c)
        {
            MockQuestionA.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockConditionA.Setup(x => x.Evaluate(It.IsAny<int>())).Returns(a);
            MockQuestionB.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(false);
            MockConditionB.Setup(x => x.Evaluate(It.IsAny<bool>())).Returns(b);
            MockQuestionC.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns("title");
            MockConditionC.Setup(x => x.Evaluate(It.IsAny<string>())).Returns(c);

            bool waitWithoutException = true;

            try
            {
                Actor.AttemptsTo(
                    Wait.Until(MockQuestionA.Object, MockConditionA.Object)
                    .And(MockQuestionB.Object, MockConditionB.Object)
                    .Or(MockQuestionC.Object, MockConditionC.Object)
                    .ForUpTo(0));
            }
            catch (WaitingException)
            {
                waitWithoutException = false;
            }

            return waitWithoutException;
        }

        [TestCase(true, true, true, ExpectedResult = true)]
        [TestCase(true, true, false, ExpectedResult = true)]
        [TestCase(true, false, true, ExpectedResult = true)]
        [TestCase(false, true, true, ExpectedResult = true)]
        [TestCase(true, false, false, ExpectedResult = true)]
        [TestCase(false, true, false, ExpectedResult = false)]
        [TestCase(false, false, true, ExpectedResult = false)]
        [TestCase(false, false, false, ExpectedResult = false)]
        public bool TestWaitOrAnd(bool a, bool b, bool c)
        {
            MockQuestionA.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockConditionA.Setup(x => x.Evaluate(It.IsAny<int>())).Returns(a);
            MockQuestionB.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(false);
            MockConditionB.Setup(x => x.Evaluate(It.IsAny<bool>())).Returns(b);
            MockQuestionC.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns("title");
            MockConditionC.Setup(x => x.Evaluate(It.IsAny<string>())).Returns(c);

            bool waitWithoutException = true;

            try
            {
                Actor.AttemptsTo(
                    Wait.Until(MockQuestionA.Object, MockConditionA.Object)
                    .Or(MockQuestionB.Object, MockConditionB.Object)
                    .And(MockQuestionC.Object, MockConditionC.Object)
                    .ForUpTo(0));
            }
            catch (WaitingException)
            {
                waitWithoutException = false;
            }

            return waitWithoutException;
        }

        [TestCase(true, true, true, ExpectedResult = true)]
        [TestCase(true, true, false, ExpectedResult = true)]
        [TestCase(true, false, true, ExpectedResult = true)]
        [TestCase(false, true, true, ExpectedResult = true)]
        [TestCase(true, false, false, ExpectedResult = true)]
        [TestCase(false, true, false, ExpectedResult = true)]
        [TestCase(false, false, true, ExpectedResult = true)]
        [TestCase(false, false, false, ExpectedResult = false)]
        public bool TestWaitOrOr(bool a, bool b, bool c)
        {
            MockQuestionA.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockConditionA.Setup(x => x.Evaluate(It.IsAny<int>())).Returns(a);
            MockQuestionB.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(false);
            MockConditionB.Setup(x => x.Evaluate(It.IsAny<bool>())).Returns(b);
            MockQuestionC.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns("title");
            MockConditionC.Setup(x => x.Evaluate(It.IsAny<string>())).Returns(c);

            bool waitWithoutException = true;

            try
            {
                Actor.AttemptsTo(
                    Wait.Until(MockQuestionA.Object, MockConditionA.Object)
                    .Or(MockQuestionB.Object, MockConditionB.Object)
                    .Or(MockQuestionC.Object, MockConditionC.Object)
                    .ForUpTo(0));
            }
            catch (WaitingException)
            {
                waitWithoutException = false;
            }

            return waitWithoutException;
        }

        [TestCase(true, true, true, true, ExpectedResult = true)]
        [TestCase(true, true, true, false, ExpectedResult = true)]
        [TestCase(true, true, false, true, ExpectedResult = true)]
        [TestCase(true, false, true, true, ExpectedResult = true)]
        [TestCase(false, true, true, true, ExpectedResult = true)]
        [TestCase(true, true, false, false, ExpectedResult = true)]
        [TestCase(true, false, true, false, ExpectedResult = false)]
        [TestCase(true, false, false, true, ExpectedResult = false)]
        [TestCase(false, true, true, false, ExpectedResult = false)]
        [TestCase(false, true, false, true, ExpectedResult = false)]
        [TestCase(false, false, true, true, ExpectedResult = true)]
        [TestCase(true, false, false, false, ExpectedResult = false)]
        [TestCase(false, true, false, false, ExpectedResult = false)]
        [TestCase(false, false, true, false, ExpectedResult = false)]
        [TestCase(false, false, false, true, ExpectedResult = false)]
        [TestCase(false, false, false, false, ExpectedResult = false)]
        public bool TestWaitAndOrAnd(bool a, bool b, bool c, bool d)
        {
            MockQuestionA.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockConditionA.Setup(x => x.Evaluate(It.IsAny<int>())).Returns(a);
            MockQuestionB.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(false);
            MockConditionB.Setup(x => x.Evaluate(It.IsAny<bool>())).Returns(b);
            MockQuestionC.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns("title");
            MockConditionC.Setup(x => x.Evaluate(It.IsAny<string>())).Returns(c);
            MockQuestionD.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns<int?>(null);
            MockConditionD.Setup(x => x.Evaluate(It.IsAny<int?>())).Returns(d);

            bool waitWithoutException = true;

            try
            {
                Actor.AttemptsTo(
                    Wait.Until(MockQuestionA.Object, MockConditionA.Object)
                    .And(MockQuestionB.Object, MockConditionB.Object)
                    .Or(MockQuestionC.Object, MockConditionC.Object)
                    .And(MockQuestionD.Object, MockConditionD.Object)
                    .ForUpTo(0));
            }
            catch (WaitingException)
            {
                waitWithoutException = false;
            }

            return waitWithoutException;
        }

        [TestCase(true, true, true, true, ExpectedResult = true)]
        [TestCase(true, true, true, false, ExpectedResult = true)]
        [TestCase(true, true, false, true, ExpectedResult = true)]
        [TestCase(true, false, true, true, ExpectedResult = true)]
        [TestCase(false, true, true, true, ExpectedResult = true)]
        [TestCase(true, true, false, false, ExpectedResult = true)]
        [TestCase(true, false, true, false, ExpectedResult = true)]
        [TestCase(true, false, false, true, ExpectedResult = true)]
        [TestCase(false, true, true, false, ExpectedResult = true)]
        [TestCase(false, true, false, true, ExpectedResult = true)]
        [TestCase(false, false, true, true, ExpectedResult = true)]
        [TestCase(true, false, false, false, ExpectedResult = true)]
        [TestCase(false, true, false, false, ExpectedResult = false)]
        [TestCase(false, false, true, false, ExpectedResult = false)]
        [TestCase(false, false, false, true, ExpectedResult = true)]
        [TestCase(false, false, false, false, ExpectedResult = false)]
        public bool TestWaitOrAndOr(bool a, bool b, bool c, bool d)
        {
            MockQuestionA.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockConditionA.Setup(x => x.Evaluate(It.IsAny<int>())).Returns(a);
            MockQuestionB.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(false);
            MockConditionB.Setup(x => x.Evaluate(It.IsAny<bool>())).Returns(b);
            MockQuestionC.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns("title");
            MockConditionC.Setup(x => x.Evaluate(It.IsAny<string>())).Returns(c);
            MockQuestionD.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns<int?>(null);
            MockConditionD.Setup(x => x.Evaluate(It.IsAny<int?>())).Returns(d);

            bool waitWithoutException = true;

            try
            {
                Actor.AttemptsTo(
                    Wait.Until(MockQuestionA.Object, MockConditionA.Object)
                    .Or(MockQuestionB.Object, MockConditionB.Object)
                    .And(MockQuestionC.Object, MockConditionC.Object)
                    .Or(MockQuestionD.Object, MockConditionD.Object)
                    .ForUpTo(0));
            }
            catch (WaitingException)
            {
                waitWithoutException = false;
            }

            return waitWithoutException;
        }

        [Test]
        public void TestSuccessfulWaitAfterChange()
        {
            const int limit = 5;
            int incrementer = 0;

            MockQuestionA.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(() => ++incrementer);
            MockConditionA.Setup(x => x.Evaluate(It.Is<int>(v => v < limit))).Returns(false);
            MockConditionA.Setup(x => x.Evaluate(It.Is<int>(v => v >= limit))).Returns(true);

            Actor.Invoking(actor => actor.AttemptsTo(Wait.Until(MockQuestionA.Object, MockConditionA.Object).ForUpTo(1)))
                .Should().NotThrow(because: "the Question should satisfy the condition");

            incrementer.Should().Be(limit, because: $"the Question should be called {limit} times");
        }

        [Test]
        public void TestSuccessfulWaitAndAndAfterChange()
        {
            const int limit = 5;
            int incrementer = 0;

            MockQuestionA.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(() => ++incrementer);
            MockConditionA.Setup(x => x.Evaluate(It.Is<int>(v => v < limit))).Returns(false);
            MockConditionA.Setup(x => x.Evaluate(It.Is<int>(v => v >= limit))).Returns(true);
            MockQuestionB.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(false);
            MockConditionB.Setup(x => x.Evaluate(It.IsAny<bool>())).Returns(true);
            MockQuestionC.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns("title");
            MockConditionC.Setup(x => x.Evaluate(It.IsAny<string>())).Returns(true);

            Actor.Invoking(actor => actor.AttemptsTo(
                Wait.Until(MockQuestionA.Object, MockConditionA.Object)
                .And(MockQuestionB.Object, MockConditionB.Object)
                .And(MockQuestionC.Object, MockConditionC.Object)
                .ForUpTo(1)))
                .Should().NotThrow(because: "the Question should satisfy the condition");

            incrementer.Should().Be(limit, because: $"the Question should be called {limit} times");
        }

        #endregion
    }
}
