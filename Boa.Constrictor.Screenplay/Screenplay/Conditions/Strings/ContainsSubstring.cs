namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Condition for containing a substring.
    /// </summary>
    public class ContainsSubstring : ICondition<string>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use the public builder method instead.)
        /// </summary>
        /// <param name="substring">The substring to seek.</param>
        private ContainsSubstring(string substring) => Substring = substring;

        #endregion

        #region Properties

        /// <summary>
        /// The substring to seek.
        /// </summary>
        public string Substring { get; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Public builder.
        /// </summary>
        /// <param name="substring">The substring to seek.</param>
        /// <returns></returns>
        public static ContainsSubstring Text(string substring) => new ContainsSubstring(substring);

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the actual string contains the expected substring.
        /// </summary>
        /// <param name="actual">The actual string.</param>
        /// <returns></returns>
        public bool Evaluate(string actual) => actual.Contains(Substring);

        /// <summary>
        /// ToString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"contains '{Substring}'";

        #endregion
    }
}
