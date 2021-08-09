namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Condition for check if a string is NULL or whitespace
    /// </summary>
    public class IsNullOrWhitespace : ICondition<string>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use the public builder method instead.)
        /// </summary>
        private IsNullOrWhitespace()
        {

        }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Public builder.
        /// </summary>
        /// <returns></returns>
        public static IsNullOrWhitespace Value() => new IsNullOrWhitespace();

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the actual value equals null or whitespace
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public bool Evaluate(string actual) => string.IsNullOrWhiteSpace(actual);

        /// <summary>
        /// ToString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"is equal to 'NULL' or Whitespace";

        #endregion
    }
}