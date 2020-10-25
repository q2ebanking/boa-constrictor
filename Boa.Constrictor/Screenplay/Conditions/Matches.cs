using System.Text.RegularExpressions;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Condition for matching a regular expression.
    /// </summary>
    public class Matches : ICondition<string>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use the public builder method instead.)
        /// </summary>
        /// <param name="regex">The regular expression to match.</param>
        private Matches(Regex regex) => RegularExpression = regex;

        #endregion

        #region Properties

        /// <summary>
        /// The regular expression to match.
        /// </summary>
        public Regex RegularExpression { get; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Public builder.
        /// </summary>
        /// <param name="regex">The regular expression to match.</param>
        /// <returns></returns>
        public static Matches Regex(Regex regex) => new Matches(regex);

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the actual string matches the regular expression.
        /// </summary>
        /// <param name="actual">The actual string.</param>
        /// <returns></returns>
        public bool Evaluate(string actual) => RegularExpression.IsMatch(actual);

        /// <summary>
        /// ToString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"matches '{RegularExpression}'";

        #endregion
    }
}
