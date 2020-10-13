namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Inverts the Boolean evaluation of another condition.
    /// </summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    public class IsNot<TValue> : ICondition<TValue>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use the public builder method instead.)
        /// </summary>
        /// <param name="inner">The inner condition.</param>
        private IsNot(ICondition<TValue> inner) => Inner = inner;

        #endregion

        #region Properties

        /// <summary>
        /// The inner condition.
        /// </summary>
        public ICondition<TValue> Inner { get; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Public builder.
        /// </summary>
        /// <param name="inner">The inner condition.</param>
        /// <returns></returns>
        public static IsNot<TValue> Condition(ICondition<TValue> inner) => new IsNot<TValue>(inner);

        #endregion

        #region Methods

        /// <summary>
        /// Evaluates a logical "NOT" on the value of the inner condition.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public bool Evaluate(TValue actual) => !Inner.Evaluate(actual);

        /// <summary>
        /// ToString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"is not {Inner}";

        #endregion
    }
}
