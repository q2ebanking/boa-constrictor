using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Condition to check if the collection size satisfies another condition.
    ///
    /// Sample usage:
    /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAcollectionOfType{string}.AndHasSizeThat(IsEqualTo.Value(10)));
    /// </summary>
    public class HasSize<T> : ICondition<IEnumerable<T>>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use the public builder method instead. <See cref="Boa.Constrictor.Screenplay.IsACollectionOfType{T}"></See> )
        /// </summary>
        /// <param name="condition">The condition to evaluate.</param>
        public HasSize(ICondition<int> condition)
        {
            Condition = condition;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The condition to evaluate.
        /// </summary>
        private ICondition<int> Condition { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Checks for a condition in the size of a collection
        /// </summary>
        /// <param name="actual">The collection to evaluate.</param>
        /// <returns>boolean</returns>
        public bool Evaluate(IEnumerable<T> actual)
        {
            return Condition.Evaluate(actual.Count());
        }

        /// <summary>
        ///     ToString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Is a collection of type {typeof(T)} and has size that {Condition}";
        }

        #endregion
    }
}