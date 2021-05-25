namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Methods to check if the collection items satifies the given conditions.
    ///</summary>
    ///<example>
    /// Sample usage:
    /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAcollectionOfType{string}.AndEveryItem(IsEqualTo.Value(link)));
    ///</example>
    public class IsACollectionOfType<T>
    {
        #region Methods 

        /// <summary>
        /// Checks for a condition in the size of a collection
        /// </summary>
        /// <example>
        /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAcollectionOfType{string}.AndHasSizeThat(IsEqualTo.Value(2)));
        /// </example>
        /// <param name="condition">The condition to evaluate.</param>
        /// <returns>HasSize</returns>
        public static HasSize<T> AndHasSizeThat(ICondition<int> condition)
        {
            return new HasSize<T>(condition);
        }

        /// <summary>
        /// Checks for a condition in every item of a collection
        /// </summary>
        /// <example>
        /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAcollectionOfType{string}.AndEveryItem(IsEqualTo.Value(link)));
        /// </example>
        /// <param name="condition">The condition to evaluate.</param>
        /// <returns>EveryItem</returns>
        public static EveryItem<T> AndEveryItem(ICondition<T> condition)
        {
            return new EveryItem<T>(condition);
        }

        /// <summary>
        /// Checks for a condition in at least one item of a collection
        /// </summary>
        /// <example>
        /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAcollectionOfType{string}.AndAtLeastOneItem(IsEqualTo.Value(link)));
        /// </example>
        /// <param name="condition">The condition to evaluate.</param>
        /// <returns>AtLeastOneItem</returns>
        public static AtLeastOneItem<T> AndAtLeastOneItem(ICondition<T> condition)
        {
            return new AtLeastOneItem<T>(condition);
        }

        /// <summary>
        /// Checks for a condition in the first item of a collection
        /// </summary>
        /// <example>
        /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAcollectionOfType{string}.AndTheFirstItem(IsEqualTo.Value(link)));
        /// </example>
        /// <param name="condition">The condition to evaluate.</param>
        /// <returns>FirstItem</returns>
        public static FirstItem<T> AndTheFirstItem(ICondition<T> condition)
        {
            return new FirstItem<T>(condition);
        }

        /// <summary>
        /// Checks for a condition in the last item of a collection
        /// </summary>
        /// <example>
        /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAcollectionOfType{string}.AndTheLastItem(IsEqualTo.Value(link)));
        /// </example>
        /// <param name="condition">The condition to evaluate.</param>
        /// <returns>LastItem</returns>
        public static LastItem<T> AndTheLastItem(ICondition<T> condition)
        {
            return new LastItem<T>(condition);
        }

        /// <summary>
        /// Checks for a condition in the item of a collection at the specified position
        /// </summary>
        /// <example>
        /// Actor.WaitsUntil(TextList.For(ResultPage.ResultLinks), IsAcollectionOfType{string}.AndTheItemAtPosition(2, IsEqualTo.Value(link)));
        /// </example>
        /// <param name="position">The position of the item to evaluate.</param>
        /// <param name="condition">The condition to evaluate.</param>
        /// <returns>ItemAtPosition</returns>
        public static ItemAtPosition<T> AndTheItemAtPosition(int position, ICondition<T> condition)
        {
            return new ItemAtPosition<T>(position, condition);
        }

        #endregion
    }
}