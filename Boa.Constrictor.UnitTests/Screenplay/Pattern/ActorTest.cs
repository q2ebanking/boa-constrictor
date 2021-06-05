using Boa.Constrictor.Screenplay;
using FluentAssertions;
using Moq;
using NUnit.Framework;

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
            actor.ToString().Should().Be($"Screenplay actor '{Actor.DefaultName}'");
        }

        [Test]
        public void CustomName()
        {
            Actor actor = new Actor("Joe");
            actor.Name.Should().Be("Joe");
            actor.ToString().Should().Be("Screenplay actor 'Joe'");
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
                .WithMessage($"Screenplay actor '{Actor.DefaultName}' does not have the ability 'Boa.Constrictor.UnitTests.Screenplay.AbilityA'");
        }

        [Test]
        public void AsksFor()
        {
            var MockQuestion = new Mock<IQuestion<bool>>();
            MockQuestion.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(true);
            new Actor().AsksFor(MockQuestion.Object).Should().BeTrue();
        }

        [Test]
        public void AskingFor()
        {
            var MockQuestion = new Mock<IQuestion<bool>>();
            MockQuestion.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(true);
            new Actor().AskingFor(MockQuestion.Object).Should().BeTrue();
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
        public void CallsTask()
        {
            bool performed = false;
            var MockTask = new Mock<ITask>();
            MockTask.Setup(x => x.PerformAs(It.IsAny<IActor>())).Callback((IActor actor) => performed = true).Verifiable();
            new Actor().Calls(MockTask.Object);
            performed.Should().BeTrue();
        }

        [Test]
        public void CallsQuestion()
        {
            var MockQuestion = new Mock<IQuestion<bool>>();
            MockQuestion.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(true);
            new Actor().Calls(MockQuestion.Object).Should().BeTrue();
        }

    }

    #endregion
}
