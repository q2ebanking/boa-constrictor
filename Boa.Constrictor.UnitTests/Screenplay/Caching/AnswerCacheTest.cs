using Boa.Constrictor.Screenplay;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    [TestFixture]
    public class AnswerCacheTest
    {
        #region Variables

        private IActor Actor { get; set; }
        private AnswerCache Cache { get; set; }
        private Mock<ICacheableQuestion<int>> MockQuestionA { get; set; }
        private Mock<ICacheableQuestion<int>> MockQuestionB { get; set; }

        #endregion

        #region Setup

        [SetUp]
        public void SetUp()
        {
            Actor = new Actor();
            Cache = new AnswerCache();

            MockQuestionA = new Mock<ICacheableQuestion<int>>();
            MockQuestionA.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(1);
            MockQuestionA.Setup(x => x.GetHashCode()).Returns(1);

            MockQuestionB = new Mock<ICacheableQuestion<int>>();
            MockQuestionB.Setup(x => x.RequestAs(It.IsAny<IActor>())).Returns(2);
            MockQuestionB.Setup(x => x.GetHashCode()).Returns(2);
        }
        
        #endregion

        #region Tests

        [Test]
        public void GetCached()
        {
            Cache.Get(MockQuestionA.Object, Actor);
            Cache.Get(MockQuestionB.Object, Actor);

            Cache.Get(MockQuestionA.Object, Actor).Should().Be(1);
            MockQuestionA.Verify(q => q.RequestAs(It.IsAny<IActor>()), Times.Once());
        }

        [Test]
        public void GetNotCached()
        {
            Cache.Get(MockQuestionA.Object, Actor).Should().Be(1);
            MockQuestionA.Verify(q => q.RequestAs(It.IsAny<IActor>()), Times.Once());
        }

        [Test]
        public void HasCached()
        {
            Cache.Get(MockQuestionA.Object, Actor);
            Cache.Get(MockQuestionB.Object, Actor);

            Cache.Has(MockQuestionA.Object).Should().BeTrue();
            Cache.Has(MockQuestionB.Object).Should().BeTrue();
        }

        [Test]
        public void HasNotCached()
        {
            Cache.Has(MockQuestionA.Object).Should().BeFalse();
            Cache.Has(MockQuestionB.Object).Should().BeFalse();
        }

        [Test]
        public void Invalidate()
        {
            Cache.Get(MockQuestionA.Object, Actor);
            Cache.Get(MockQuestionB.Object, Actor);

            Cache.Invalidate(MockQuestionA.Object);
            Cache.Has(MockQuestionA.Object).Should().BeFalse();
            Cache.Has(MockQuestionB.Object).Should().BeTrue();
        }

        [Test]
        public void InvalidateAll()
        {
            Cache.Get(MockQuestionA.Object, Actor);
            Cache.Get(MockQuestionB.Object, Actor);

            Cache.InvalidateAll();
            Cache.Has(MockQuestionA.Object).Should().BeFalse();
            Cache.Has(MockQuestionB.Object).Should().BeFalse();
        }

        #endregion
    }
}
