using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Condition to check if the first item on an enumerable satisfies another.
    /// </summary>
    /// <example>
    /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAnEnumerable{string}.WhereTheFirstItem(IsEqualTo.Value(link)));
    /// </example>
    public class FirstItem<T> : ICondition<IEnumerable<T>>
    {
        #region Constructors

        /// <summary>
        /// Internal constructor.
        /// (Use the public builder method instead. <See cref="Boa.Constrictor.Screenplay.IsAnEnumerable{T}"></See> )
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
        /// Checks for a condition in the first item of an enumerable.
        /// </summary>
        /// <param name="actual">The enumerable to evaluate.</param>
        /// <returns>boolean</returns>
        public bool Evaluate(IEnumerable<T> actual) => Condition.Evaluate(actual.First());

        /// <summary>
        ///     ToString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"is an IEnumerable<{typeof(T)}> where the first item {Condition}";

        #endregion
    }
}