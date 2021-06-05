namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Builder methods for Conditions that check if the enumerable items satifies the given conditions.
    ///</summary>
    ///<example>
    /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAnEnumerable{string}.WhereEveryItem(IsEqualTo.Value(link)));
    ///</example>
    public class IsAnEnumerable<T>
    {
        #region Methods 

        /// <summary>
        /// Checks for a condition in the count of an enumerable.
        /// </summary>
        /// <example>
        /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAnEnumerable{string}.WhereTheCount(IsEqualTo.Value(2)));
        /// </example>
        /// <param name="condition">The condition to evaluate.</param>
        /// <returns>Count</returns>
        public static Count<T> WhereTheCount(ICondition<int> condition) => new Count<T>(condition);

        /// <summary>
        /// Checks if a condition is satisfied in every item of an enumerable.
        /// </summary>
        /// <example>
        /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAnEnumerable{string}.WhereEveryItem(IsEqualTo.Value(link)));
        /// </example>
        /// <param name="condition">The condition to evaluate.</param>
        /// <returns>EveryItem</returns>
        public static EveryItem<T> WhereEveryItem(ICondition<T> condition) => new EveryItem<T>(condition);

        /// <summary>
        /// Checks if a condition is satisfied in at least one item of an enumerable.
        /// </summary>
        /// <example>
        /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAnEnumerable{string}.WhereAtLeastOneItem(IsEqualTo.Value(link)));
        /// </example>
        /// <param name="condition">The condition to evaluate.</param>
        /// <returns>AtLeastOneItem</returns>
        public static AtLeastOneItem<T> WhereAtLeastOneItem(ICondition<T> condition) => 
            new AtLeastOneItem<T>(condition);

        /// <summary>
        /// Checks for a condition in the first item of an enumerable.
        /// </summary>
        /// <example>
        /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAnEnumerable{string}.WhereTheFirstItem(IsEqualTo.Value(link)));
        /// </example>
        /// <param name="condition">The condition to evaluate.</param>
        /// <returns>FirstItem</returns>
        public static FirstItem<T> WhereTheFirstItem(ICondition<T> condition) => new FirstItem<T>(condition);

        /// <summary>
        /// Checks for a condition in the last item of an enumerable.
        /// </summary>
        /// <example>
        /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAnEnumerable{string}.WhereTheLastItem(IsEqualTo.Value(link)));
        /// </example>
        /// <param name="condition">The condition to evaluate.</param>
        /// <returns>LastItem</returns>
        public static LastItem<T> WhereTheLastItem(ICondition<T> condition) => new LastItem<T>(condition);

        /// <summary>
        /// Checks for a condition in the item at the specified position of an enumerable.
        /// </summary>
        /// <example>
        /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAnEnumerable{string}.WhereTheItemAtPosition(2, IsEqualTo.Value(link)));
        /// </example>
        /// <param name="position">The position of the item to evaluate.</param>
        /// <param name="condition">The condition to evaluate.</param>
        /// <returns>ItemAtPosition</returns>
        public static ItemAtPosition<T> WhereTheItemAtPosition(int position, ICondition<T> condition) => 
            new ItemAtPosition<T>(position, condition);

        #endregion
    }
}