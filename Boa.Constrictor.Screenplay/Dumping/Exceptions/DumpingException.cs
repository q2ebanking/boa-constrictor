using System;

namespace Boa.Constrictor.Dumping
{
    /// <summary>
    /// Should be used for any dumping exceptions.
    /// </summary>
    public class DumpingException : Exception
    {
        #region Constructors

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public DumpingException() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public DumpingException(string message) : base(message) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="inner">The inner exception.</param>
        public DumpingException(string message, Exception inner) : base(message, inner) { }

        #endregion
    }
}
