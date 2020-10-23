using System;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Should be used for any Screenplay Pattern exceptions.
    /// </summary>
    public class ScreenplayException : Exception
    {
        #region Constructors

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public ScreenplayException() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ScreenplayException(string message) : base(message) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="inner">The inner exception.</param>
        public ScreenplayException(string message, Exception inner) : base(message, inner) { }

        #endregion
    }
}
