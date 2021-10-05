namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// This exception should be thrown when the Wait interaction fails to meet its expected condition.
    /// It provides attributes for the actual value, the question, and the condition.
    /// </summary>
    public class WaitingException : ScreenplayException
    {
        #region Constructors

        /// <summary>
        /// Most basic constructor.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="interaction">The waiting interaction.</param>
        /// <param name="actual">The actual value received after waiting.</param>
        private WaitingException(string message, AbstractWait interaction, string actual) :
            base(message)
        {
            Interaction = interaction;
            Value = actual;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interaction">The waiting interaction.</param>
        /// <param name="actual">The actual value received after waiting.</param>
        public WaitingException(AbstractWait interaction, string actual) :
            this($"{interaction} timed out yielding '{actual}'", interaction, actual)
        { }

        #endregion

        #region Properties

        /// <summary>
        /// The actual value received after waiting.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// The waiting interaction.
        /// </summary>
        public AbstractWait Interaction { get; }

        #endregion
    }
}