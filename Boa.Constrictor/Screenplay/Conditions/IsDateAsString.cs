using System;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Condition for checking if two strings are equal based on the dates they represent.
    /// </summary>
    public class IsDateAsString : ICondition<string>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use the public builder method instead.)
        /// </summary>
        /// <param name="expected">The expected value.</param>
        private IsDateAsString(string expected) => Expected = expected;

        #endregion

        #region Properties

        /// <summary>
        /// The expected value.
        /// </summary>
        public string Expected { get; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Public builder.
        /// </summary>
        /// <param name="expected">The expected date as a string.</param>
        /// <returns></returns>
        public static IsDateAsString Value(string expected) => new IsDateAsString(expected);

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the two strings are equal based on the dates they represent.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public bool Evaluate(string actual) => DateTime.Parse(actual) == DateTime.Parse(Expected);

        /// <summary>
        /// ToString override.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"equals '{Expected}' as a date";

        #endregion
    }
}
