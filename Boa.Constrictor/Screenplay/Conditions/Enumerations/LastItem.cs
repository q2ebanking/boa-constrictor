using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.Screenplay
{

    /// <summary>
    /// Condition to check if the enumerable last item satisfies another condition.
    /// </summary>
    /// <example>
    /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAnEnumerable{string}.WhereTheLastItem(IsEqualTo.Value(10)));
    /// </example>
    public class LastItem<T> : ICondition<IEnumerable<T>>
    {

        #region Constructors

        /// <summary>
        /// Internal constructor.
        /// (Use the public builder method instead. <See cref="Boa.Constrictor.Screenplay.IsAnEnumerable{T}"></See> )
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
        ///     Checks for a condition in the last item of an enumerable.
        /// </summary>
        /// <param name="actual">The enumerable to evaluate.</param>
        /// <returns></returns>
        public bool Evaluate(IEnumerable<T> actual) => Condition.Evaluate(actual.Last());

        /// <summary>
        ///     ToString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"is an IEnumerable<{typeof(T)}> where the last item {Condition}";

        #endregion
    }
}