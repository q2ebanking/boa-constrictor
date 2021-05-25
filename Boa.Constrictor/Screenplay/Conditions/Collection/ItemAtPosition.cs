using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Condition to check if the collection item at a position satisfies another condition.
    ///
    /// Sample usage:
    /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAcollectionOfType{string}.AndTheItemAtPosition(0, IsEqualTo.Value(10)));
    /// </summary>
    public class ItemAtPosition<T> : ICondition<IEnumerable<T>>
    {

        #region Constructors

        /// <summary>
        /// Internal constructor.
        /// (Use the public builder method instead. <See cref="Boa.Constrictor.Screenplay.IsACollectionOfType{T}"></See> )
        /// </summary>
        /// <param name="index">The index of the item to evaluate.</param>
        /// <param name="condition">The condition to evaluate.</param>
        internal ItemAtPosition(int index, ICondition<T> condition)
        {
            Condition = condition;
            Index = index;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The condition to evaluate.
        /// </summary>
        private ICondition<T> Condition { get; }
        /// <summary>
        /// The index of the Item to evaluate a condition.
        /// </summary>
        private int Index { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Checks for a condition in the item at a position of a collection
        /// </summary>
        /// <param name="actual">The collection to evaluate.</param>
        /// <returns>boolean</returns>
        public bool Evaluate(IEnumerable<T> actual)
        {
            if (actual.Count() <= Index || Index < 0) return false;
            return Condition.Evaluate(actual.ElementAt(Index));
        }

        /// <summary>
        ///     ToString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Is a collection of type {typeof(T)} and the item at position {Index} {Condition}";
        }

        #endregion
    }
}