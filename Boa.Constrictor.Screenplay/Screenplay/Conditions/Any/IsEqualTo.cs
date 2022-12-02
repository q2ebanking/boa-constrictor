namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Condition for equality.
    /// Uses the Equals method.
    /// </summary>
    /// <typeparam name="TValue">The expected value type.</typeparam>
    public class IsEqualTo<TValue> : ICondition<TValue>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use the public builder method instead.)
        /// </summary>
        /// <param name="expected">The expected value.</param>
        private IsEqualTo(TValue expected) => Expected = expected;

        #endregion

        #region Properties

        /// <summary>
        /// The expected value.
        /// </summary>
        public TValue Expected { get; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Public builder.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <returns></returns>
        public static IsEqualTo<TValue> Value(TValue expected) => new IsEqualTo<TValue>(expected);

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the actual value equals the expected value using the "Equals" method.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public bool Evaluate(TValue actual) => actual?.Equals(Expected) ?? Expected == null;

        /// <summary>
        /// ToString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"is equal to '{Expected}'";

        #endregion
    }

    /// <summary>
    /// Provides builder methods for IsEqualTo conditions without type generics.
    /// </summary>
    public static class IsEqualTo
    {
        /// <summary>
        /// Builder method to avoid requiring type generic in the fluent call.
        /// </summary>
        /// <returns></returns>
        public static IsEqualTo<bool> False() => IsEqualTo<bool>.Value(false);

        /// <summary>
        /// Builder method to avoid requiring type generic in the fluent call.
        /// </summary>
        /// <returns></returns>
        public static IsEqualTo<bool> True() => IsEqualTo<bool>.Value(true);

        /// <summary>
        /// Builder method to avoid requiring type generic in the fluent call.
        /// </summary>
        /// <typeparam name="TValue">The expected value type.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <returns></returns>
        public static IsEqualTo<TValue> Value<TValue>(TValue expected) => IsEqualTo<TValue>.Value(expected);
    }

    /// <summary>
    /// Provides builder methods for adding a logical NOT to IsEqualTo conditions.
    /// </summary>
    public static class IsNotEqualTo
    {
        /// <summary>
        /// Builder method to avoid requiring type generic in the fluent call.
        /// </summary>
        /// <typeparam name="TValue">The expected value type.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <returns></returns>
        public static IsNot<TValue> Value<TValue>(TValue expected) => IsNot<TValue>.Condition(IsEqualTo<TValue>.Value(expected));
    }
}
