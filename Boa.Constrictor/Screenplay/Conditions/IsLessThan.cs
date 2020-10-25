using System;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Condition for comparison.
    /// </summary>
    /// <typeparam name="TValue">The value type, which must be comparable.</typeparam>
    public class IsLessThan<TValue> : AbstractComparison<TValue>, ICondition<TValue>
        where TValue : IComparable<TValue>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use the public builder method instead.)
        /// </summary>
        /// <param name="value">The comparison value.</param>
        private IsLessThan(TValue value) : base(value) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Public builder.
        /// </summary>
        /// <param name="value">The comparison value.</param>
        /// <returns></returns>
        public static IsLessThan<TValue> Value(TValue value) => new IsLessThan<TValue>(value);

        #endregion

        #region Methods

        /// <summary>
        /// Compares the actual value to the comparison value.
        /// </summary>
        /// <param name="actual">The actual value.</param>
        /// <returns></returns>
        public bool Evaluate(TValue actual) => Compare(actual) < 0;

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"< {Expected}";

        #endregion
    }

    /// <summary>
    /// Provides builder methods for IsLessThan conditions without type generics.
    /// </summary>
    public static class IsLessThan
    {
        /// <summary>
        /// Builder method to avoid requiring type generic in the fluent call.
        /// </summary>
        /// <typeparam name="TValue">The expected value type.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <returns></returns>
        public static IsLessThan<TValue> Value<TValue>(TValue expected) where TValue : IComparable<TValue> =>
            IsLessThan<TValue>.Value(expected);
    }

    /// <summary>
    /// Provides builder methods for adding a logical NOT to IsLessThan conditions.
    /// </summary>
    public static class IsGreaterThanOrEqualTo
    {
        /// <summary>
        /// Builder method to avoid requiring type generic in the fluent call.
        /// </summary>
        /// <typeparam name="TValue">The expected value type.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <returns></returns>
        public static IsNot<TValue> Value<TValue>(TValue expected) where TValue : IComparable<TValue> =>
            IsNot<TValue>.Condition(IsLessThan<TValue>.Value(expected));
    }
}
