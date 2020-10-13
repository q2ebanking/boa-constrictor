using System;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Should be used for any Screenplay Pattern exceptions.
    /// </summary>
    public class ScreenplayException : Exception
    {
        #region Constructors

        public ScreenplayException() { }
        public ScreenplayException(string message) : base(message) { }
        public ScreenplayException(string message, Exception inner) : base(message, inner) { }

        #endregion
    }
}
