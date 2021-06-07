using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Condition to check if the enumerable count satisfies another condition.
    /// </summary>
    /// <example>
    /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAnEnumerable{string}.WhereTheCount(IsEqualTo.Value(10)));
    /// </example>
    public class Count<T> : ICondition<IEnumerable<T>>
    {
        #region Constructors

        /// <summary>
        /// Internal constructor.
        /// (Use the public builder method instead. <See cref="Boa.Constrictor.Screenplay.IsAnEnumerable{T}"></See> )
        /// </summary>
        /// <param name="condition">The condition to evaluate.</param>
        internal Count(ICondition<int> condition)
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
        /// Checks for a condition in the count of an enumerable.
        /// </summary>
        /// <param name="actual">The enumerable to evaluate.</param>
        /// <returns>boolean</returns>
        public bool Evaluate(IEnumerable<T> actual) => Condition.Evaluate(actual.Count());

        /// <summary>
        ///     ToString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"is an IEnumerable<{typeof(T)}> where count {Condition}";

        #endregion
    }
}