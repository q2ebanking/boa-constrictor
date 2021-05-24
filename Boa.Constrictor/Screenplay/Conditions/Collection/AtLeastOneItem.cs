using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Condition to check if at least one Item on a collection satisfies another condition.
    ///
    /// Sample Usage:
    /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAcollectionOfType{string}.AndAtLeastOneItem(IsEqualTo.Value(link)));
    /// </summary>
    public class AtLeastOneItem<T> : ICondition<IEnumerable<T>>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use the public builder method instead. <See cref="Boa.Constrictor.Screenplay.IsACollectionOfType{T}"></See>)
        /// </summary>
        /// <param name="condition">The condition to evaluate.</param>
        public AtLeastOneItem(ICondition<T> condition)
        {
            Condition = condition;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The condition to evaluate.
        /// </summary>
        private ICondition<T> Condition { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Checks for a condition in at least one item of a collection
        /// </summary>
        /// <param name="actual">The collection to evaluate.</param>
        /// <returns>boolean</returns>
        public bool Evaluate(IEnumerable<T> actual)
        {
            List<bool> results = new List<bool>();
            foreach (var item in actual)
            {
                results.Add(Condition.Evaluate(item));
            }
            return results.Any(result => result.Equals(true));
        }

        /// <summary>
        ///     ToString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Is a collection of type {typeof(T)} and at least one item {Condition}";
        }

        #endregion
    }
}