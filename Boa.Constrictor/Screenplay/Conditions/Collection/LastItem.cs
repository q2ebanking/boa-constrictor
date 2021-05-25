using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.Screenplay
{

    /// <summary>
    /// Condition to check if the collection last item satisfies another condition.
    ///
    /// Sample usage:
    /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAcollectionOfType{string}.AndTheLastItem(IsEqualTo.Value(10)));
    /// </summary>
    public class LastItem<T> : ICondition<IEnumerable<T>>
    {

        #region Constructors

        /// <summary>
        /// Internal constructor.
        /// (Use the public builder method instead. <See cref="Boa.Constrictor.Screenplay.IsACollectionOfType{T}"></See> )
        /// </summary>
        /// <param name="condition">The condition to evaluate.</param>
        internal LastItem(ICondition<T> condition)
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
        ///     Checks for a condition in the last item of a collection
        /// </summary>
        /// <param name="actual">The collection to evaluate.</param>
        /// <returns></returns>
        public bool Evaluate(IEnumerable<T> actual)
        {

            return Condition.Evaluate(actual.Last());
        }

        /// <summary>
        ///     ToString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Is a collection of type {typeof(T)} and the last item {Condition}";
        }

        #endregion
    }
}