using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Condition to check if every item on an enumerable satisfies.
    /// </summary>
    /// <example>
    /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAnEnumerable{string}.WhereEveryItem(IsEqualTo.Value(link)));
    /// </example>
    public class EveryItem<T> : ICondition<IEnumerable<T>>
    {
        #region Constructors

        /// <summary>
        /// Internal constructor.
        /// (Use the public builder method instead. <See cref="Boa.Constrictor.Screenplay.IsAnEnumerable{T}"></See>)
        /// </summary>
        /// <param name="condition">The condition to evaluate.</param>
        internal EveryItem(ICondition<T> condition)
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
        /// Checks if a condition is satisfied in every item of an enumerable.
        /// </summary>
        /// <param name="actual">The enumerable to evaluate.</param>
        /// <returns>boolean</returns>
        public bool Evaluate(IEnumerable<T> actual) => 
            actual.Select(item => Condition.Evaluate(item)).All(result => result.Equals(true));

        /// <summary>
        ///     ToString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"is an IEnumerable<{typeof(T)}> where every item {Condition}";

        #endregion
    }
}