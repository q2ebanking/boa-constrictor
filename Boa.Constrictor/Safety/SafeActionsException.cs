using System;
using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.Safety
{
    /// <summary>
    /// This exception occurs when safe actions have failures.
    /// The message is the combination of all failure messages.
    /// </summary>
    public class SafeActionsException : Exception
    {
        #region Private Class Methods

        /// <summary>
        /// Concatenates the failure messages together.
        /// </summary>
        /// <param name="failures">Collection of failure exceptions.</param>
        /// <returns></returns>
        private static string ConcatMessage(IEnumerable<Exception> failures)
        {
            int count = 1;
            return (failures.Count() == 0)
                ? "(No failures provided)"
                : string.Join("; ", failures.Select(e => $"({count++}) {e.Message}"));
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for exception collection.
        /// </summary>
        /// <param name="failures">Collection of failure exceptions.</param>
        public SafeActionsException(IEnumerable<Exception> failures) :
            base(ConcatMessage(failures))
        {
            Failures = failures;
        }

        /// <summary>
        /// Constructor for exception params.
        /// </summary>
        /// <param name="failures">Collection of failure exceptions.</param>
        public SafeActionsException(params Exception[] failures) :
            this((IEnumerable<Exception>)failures)
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Collection of failure exceptions.
        /// </summary>
        public IEnumerable<Exception> Failures { get; }

        #endregion
    }
}
