using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Condition to check if the first item on a collection satisfies another condition.
    ///
    /// Sample usage:
    /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAcollectionOfType{string}.AndTheFirstItem(IsEqualTo.Value(link)));
    /// </summary>
    public class FirstItem<T> : ICondition<IEnumerable<T>>
    {
        #region Constructors

        /// <summary>
        /// Internal constructor.
        /// (Use the public builder method instead. <See cref="Boa.Constrictor.Screenplay.IsACollectionOfType{T}"></See> )
        /// </summary>
        /// <param name="condition">The condition to evaluate.</param>
        internal FirstItem(ICondition<T> condition)
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
        /// Checks for a condition in the first item of a collection
        /// </summary>
        /// <param name="actual">The collection to evaluate.</param>
        /// <returns>boolean</returns>
        public bool Evaluate(IEnumerable<T> actual)
        {

            return Condition.Evaluate(actual.First());
        }

        /// <summary>
        ///     ToString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Is a collection of type {typeof(T)} and the first item {Condition}";
        }

        #endregion
    }
}