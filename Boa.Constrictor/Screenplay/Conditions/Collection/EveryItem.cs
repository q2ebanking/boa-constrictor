using System.Collections.Generic;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Condition to check if every item on a collection satisfies another condition.
    ///
    /// Sample usage:
    /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAcollectionOfType{string}.AndEveryItem(IsEqualTo.Value(link)));
    /// </summary>
    public class EveryItem<T> : ICondition<IEnumerable<T>>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use the public builder method instead. <See cref="Boa.Constrictor.Screenplay.IsACollectionOfType{T}"></See>)
        /// </summary>
        /// <param name="condition">The condition to evaluate.</param>
        public EveryItem(ICondition<T> condition)
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
        /// Checks for a condition in every item of a collection
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
            return results.TrueForAll(result => result.Equals(true));
        }

        /// <summary>
        ///     ToString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Is a collection of type {typeof(T)} and every item {Condition}";
        }

        #endregion
    }
}