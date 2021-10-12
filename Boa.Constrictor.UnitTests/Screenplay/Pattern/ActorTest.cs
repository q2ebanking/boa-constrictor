using Boa.Constrictor.Screenplay;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    #region Ability Helper Classes

    public class AbilityA : IAbility { }

    public class AbilityB : IAbility { }

    #endregion

    #region Test Class

    [TestFixture]
    public class ActorTest
    {
        [Test]
        public void DefaultName()
        {
            Actor actor = new Actor();
            actor.Name.Should().Be(Actor.DefaultName);
            actor.ToString().Should().Be($"Screenplay Actor '{Actor.DefaultName}'");
        }

        [Test]
        public void CustomName()
        {
            Actor actor = new Actor("Joe");
            actor.Name.Should().Be("Joe");
            actor.ToString().Should().Be("Screenplay Actor 'Joe'");
        }

        [Test]
        public void HasAbilityTrue()
        {
            Actor actor = new Actor();
            actor.Can(new AbilityA());
            actor.HasAbilityTo<AbilityA>().Should().BeTrue();
        }

        [Test]
        public void HasAbilityFalse()
        {
            new Actor().HasAbilityTo<AbilityA>().Should().BeFalse();
        }

        [Test]
        public void CannotAddSameAbilityTwice()
        {
            Actor actor = new Actor();
            IAbility one = new AbilityA();
            IAbility two = new AbilityA();

            actor.Can(one);
            actor.Invoking(a => a.Can(two)).Should().Throw<System.ArgumentException>();
        }

        [Test]
        public void UsingAvailableAbility()
        {
            Actor actor = new Actor();
            IAbility a = new AbilityA();
            IAbility b = new AbilityB();

            actor.Can(a);
            actor.Can(b);

            actor.Using<AbilityA>().Should().BeSameAs(a);
            actor.Using<AbilityB>().Should().BeSameAs(b);
        }

        [Test]
        public void UsingUnavailableAbility()
        {
            new Actor().Invoking(x => x.Using<AbilityA>()).Should()
                .Throw<ScreenplayException>()
                .WithMessage($"Screenplay Actor '{Actor.DefaultName}' does not have the Ability 'Boa.Constrictor.UnitTests.Screenplay.AbilityA'");
        }

        [Test]
        public void AsksFor()
        {
            var MockQuestion = new Mock<IQuestion<bool>>();
            MockQuestion.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(true);
            new Actor().AsksFor(MockQuestion.Object).Should().BeTrue();
        }

        [Test]
        public async Task AsksForAsync()
        {
            var MockQuestion = new Mock<IQuestionAsync<bool>>();
            MockQuestion.Setup(x => x.RequestAsAsync(It.IsAny<IActor>())).ReturnsAsync(true);
            bool answer = await new Actor().AsksForAsync(MockQuestion.Object);
            answer.Should().BeTrue();
        }

        [Test]
        public void AskingFor()
        {
            var MockQuestion = new Mock<IQuestion<bool>>();
            MockQuestion.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(true);
            new Actor().AskingFor(MockQuestion.Object).Should().BeTrue();
        }

        [Test]
        public async Task AskingForAsync()
        {
            var MockQuestion = new Mock<IQuestionAsync<bool>>();
            MockQuestion.Setup(x => x.RequestAsAsync(It.IsAny<IActor>())).ReturnsAsync(true);
            bool answer = await new Actor().AskingForAsync(MockQuestion.Object);
            answer.Should().BeTrue();
        }

        [Test]
        public void AttemptsTo()
        {
            bool performed = false;
            var MockTask = new Mock<ITask>();
            MockTask.Setup(x => x.PerformAs(It.IsAny<IActor>())).Callback((IActor actor) => performed = true).Verifiable();
            new Actor().AttemptsTo(MockTask.Object);
            performed.Should().BeTrue();
        }

        [Test]
        public async Task AttemptsToAsync()
        {
            bool performed = false;
            var MockTask = new Mock<ITaskAsync>();
            MockTask.Setup(x => x.PerformAsAsync(It.IsAny<IActor>())).Callback((IActor actor) => performed = true);
            await new Actor().AttemptsToAsync(MockTask.Object);
            performed.Should().BeTrue();
        }

        [Test]
        public void AttemptsToPerformMultipleTasks()
        {
            int performCount = 0;
            var MockTask = new Mock<ITask>();
            MockTask.Setup(x => x.PerformAs(It.IsAny<IActor>())).Callback((IActor actor) => performCount++).Verifiable();
            ITask[] tasks = new ITask[] { MockTask.Object, MockTask.Object, MockTask.Object };
            new Actor().AttemptsTo(tasks);
            performCount.Should().Be(tasks.Length);
        }

        [Test]
        public void AttemptsToPerformMultipleTasksParams()
        {
            int performCount = 0;
            var MockTask = new Mock<ITask>();
            MockTask.Setup(x => x.PerformAs(It.IsAny<IActor>())).Callback((IActor actor) => performCount++).Verifiable();
            new Actor().AttemptsTo(MockTask.Object, MockTask.Object, MockTask.Object);
            performCount.Should().Be(3);
        }

        [Test]
        public void CallsQuestion()
        {
            var MockQuestion = new Mock<IQuestion<bool>>();
            MockQuestion.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(true);
            new Actor().Calls(MockQuestion.Object).Should().BeTrue();
        }

        [Test]
        public async Task CallsQuestionAsync()
        {
            var MockQuestion = new Mock<IQuestionAsync<bool>>();
            MockQuestion.Setup(x => x.RequestAsAsync(It.IsAny<IActor>())).ReturnsAsync(true);
            bool answer = await new Actor().CallsAsync(MockQuestion.Object);
            answer.Should().BeTrue();
        }

        [Test]
        public void CallsTask()
        {
            bool performed = false;
            var MockTask = new Mock<ITask>();
            MockTask.Setup(x => x.PerformAs(It.IsAny<IActor>())).Callback((IActor actor) => performed = true).Verifiable();
            new Actor().Calls(MockTask.Object);
            performed.Should().BeTrue();
        }

        [Test]
        public async Task CallsTaskAsync()
        {
            bool performed = false;
            var MockTask = new Mock<ITaskAsync>();
            MockTask.Setup(x => x.PerformAsAsync(It.IsAny<IActor>())).Callback((IActor actor) => performed = true);
            await new Actor().CallsAsync(MockTask.Object);
            performed.Should().BeTrue();
        }

    }

    #endregion
}
