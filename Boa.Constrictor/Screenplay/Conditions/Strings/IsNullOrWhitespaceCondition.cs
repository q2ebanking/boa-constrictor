namespace Boa.Constrictor.Screenplay
{
    public class IsNullOrWhitespaceCondition : ICondition<string>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use the public builder method instead.)
        /// </summary>
        private IsNullOrWhitespaceCondition()
        {

        }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Public builder.
        /// </summary>
        /// <returns></returns>
        public static IsNullOrWhitespaceCondition Value() => new IsNullOrWhitespaceCondition();

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the actual value equals null or whitespace
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public bool Evaluate(string actual)
        {
            return string.IsNullOrWhiteSpace(actual);
        }

        /// <summary>
        /// ToString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"is equal to 'NULL' or Whitespace";

        #endregion
    }
}