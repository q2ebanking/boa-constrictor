using Boa.Constrictor.Screenplay;
using System;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Should be used for any Screenplay WebDriver interaction exceptions.
    /// </summary>
    public class BrowserInteractionException : ScreenplayException
    {
        #region Constructors

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public BrowserInteractionException() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public BrowserInteractionException(string message) : base(message) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="inner">The inner exception.</param>
        public BrowserInteractionException(string message, Exception inner) : base(message, inner) { }

        #endregion
    }
}
