using System;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Condition superclass for comparisons.
    /// </summary>
    /// <typeparam name="TValue">The value type, which must be comparable.</typeparam>
    public abstract class AbstractComparison<TValue> where TValue : IComparable<TValue>
    {
        #region Constructors

        /// <summary>
        /// Protected constructor.
        /// (Use the public builder method instead.)
        /// </summary>
        /// <param name="expected">The comparison value.</param>
        protected AbstractComparison(TValue expected) => Expected = expected;

        #endregion

        #region Properties

        /// <summary>
        /// The comparison value.
        /// </summary>
        public TValue Expected { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Checks how the actual value compares to the other value.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public int Compare(TValue actual) => actual.CompareTo(Expected);

        #endregion
    }
}
