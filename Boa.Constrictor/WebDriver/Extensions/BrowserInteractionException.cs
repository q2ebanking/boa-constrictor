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

        public BrowserInteractionException() { }
        public BrowserInteractionException(string message) : base(message) { }
        public BrowserInteractionException(string message, Exception inner) : base(message, inner) { }

        #endregion
    }
}
