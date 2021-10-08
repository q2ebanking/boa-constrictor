using System.Collections.Generic;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// This exception should be thrown when the Wait interaction fails to meet its expected condition.
    /// It provides attributes for the values, the Question, and the condition.
    /// </summary>
    public class WaitingException : ScreenplayException
    {
        #region Constructors

        /// <summary>
        /// Most basic constructor.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="interaction">The waiting interaction.</param>
        /// <param name="values">The values received after waiting.</param>
        protected WaitingException(string message, AbstractWait interaction, List<object> values) :
            base(message)
        {
            Interaction = interaction;
            Values = values;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interaction">The waiting interaction.</param>
        /// <param name="values">The values received after waiting.</param>
        public WaitingException(AbstractWait interaction, List<object> values) :
            this($"{interaction} timed out yielding '{string.Join(", ", values)}'", interaction, values)
        { }

        #endregion

        #region Properties

        /// <summary>
        /// The values received after waiting.
        /// </summary>
        public List<object> Values { get; } = new List<object>();

        /// <summary>
        /// The waiting interaction.
        /// </summary>
        public AbstractWait Interaction { get; }

        #endregion
    }

    /// <summary>
    /// This exception should be thrown when the Wait interaction fails to meet its expected condition.
    /// It provides attributes for the actual value, the Question, and the condition.
    /// </summary>
    /// <typeparam name="TAnswer"></typeparam>
    public class WaitingException<TAnswer> : WaitingException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interaction">The waiting interaction.</param>
        /// <param name="actual">The actual value received after waiting.</param>
        public WaitingException(AbstractWait interaction, TAnswer actual) :
            base($"{interaction} timed out yielding '{actual}'", interaction, new List<object>() { actual })
        { }

        /// <summary>
        /// The actual value received after waiting.
        /// </summary>
        public TAnswer ActualValue => (TAnswer)Values[0];
    }
}