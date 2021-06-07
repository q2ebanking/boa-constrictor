using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace Boa.Constrictor.UnitTests.Screenplay
{
    [TestFixture]
    public class ItemAtPositionTest
    {
        #region Tests

        [Test]
        public void WhereTheItemAtPositionIsEqualToValue_WithItemAtPositionEqual_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereTheItemAtPosition(0, IsEqualTo.Value(1)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void WhereTheItemAtPositionIsEqualToValue_WithItemAtPositionNotEqual_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereTheItemAtPosition(2, IsEqualTo.Value(2)).Evaluate(array).Should().BeFalse();
        }

        [Test]
        public void WhereTheItemAtPositionIsEqualToValue_WithNotExistentItemInPosition_ShouldThrowException()
        {
            int[] array = { 1, 2, 3 };
            int index = 3;
            Action act = () => IsAnEnumerable<int>.WhereTheItemAtPosition(index, IsEqualTo.Value(3)).Evaluate(array);
            act.Should().Throw<ScreenplayException>()
                 .WithMessage($"Index {index} is out of range for the IEnumerable<{typeof(int)}> with count {array.Count()}");
        }

        [Test]
        public void WhereTheItemAtPositionIsEqualToValue_WithNegativePosition_ShouldThrowException()
        {
            int[] array = { 1, 2, 3 };
            int index = -1;
            Action act = () => IsAnEnumerable<int>.WhereTheItemAtPosition(index, IsEqualTo.Value(1)).Evaluate(array);
            act.Should().Throw<ScreenplayException>()
                 .WithMessage($"Index {index} is out of range for the IEnumerable<{typeof(int)}> with count {array.Count()}");
        }

        [Test]
        public void WhereTheItemAtPositionIsEqualToValue_WithJustOneItemAndNotExistentItemInPosition_ShouldThrowException()
        {
            int[] array = { 1 };
            int index = 1;
            Action act = () => IsAnEnumerable<int>.WhereTheItemAtPosition(index, IsEqualTo.Value(1)).Evaluate(array);
            act.Should().Throw<ScreenplayException>()
                 .WithMessage($"Index {index} is out of range for the IEnumerable<{typeof(int)}> with count {array.Count()}");
        }

        [Test]
        public void WhereTheItemAtPositionIsGreaterThanValue_WithItemAtPositionGreater_ShouldBeTrue()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereTheItemAtPosition(0, IsGreaterThan.Value(0)).Evaluate(array).Should().BeTrue();
        }

        [Test]
        public void WhereTheItemAtPositionIsGreaterThanValue_WithItemAtPositionNotGreater_ShouldBeFalse()
        {
            int[] array = { 1, 2, 3 };
            IsAnEnumerable<int>.WhereTheItemAtPosition(1, IsGreaterThan.Value(2)).Evaluate(array).Should().BeFalse();
        }

        [Test]
        public void WhereTheItemAtPositionIsGreaterThanValue_WithNotExistentItemInPosition_ShouldThrowException()
        {
            int[] array = { 1, 2, 3 };
            int index = 3;
            Action act = () => IsAnEnumerable<int>.WhereTheItemAtPosition(index, IsGreaterThan.Value(2)).Evaluate(array);
            act.Should().Throw<ScreenplayException>()
                 .WithMessage($"Index {index} is out of range for the IEnumerable<{typeof(int)}> with count {array.Count()}");
        }

        #endregion
    }
}
