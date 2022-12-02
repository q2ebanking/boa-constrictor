using System;

namespace Boa.Constrictor.Logging
{
    /// <summary>
    /// Should be used for any Boa Constrictor logging exceptions.
    /// </summary>
    public class LoggingException : Exception
    {
        #region Constructors

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public LoggingException() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public LoggingException(string message) : base(message) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="inner">The inner exception.</param>
        public LoggingException(string message, Exception inner) : base(message, inner) { }

        #endregion
    }
}
