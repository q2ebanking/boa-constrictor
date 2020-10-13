namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// This exception should be thrown when the Wait interaction fails to meet its expected condition.
    /// It provides attributes for the actual value, the question, and the condition.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class WaitingException<TValue> : ScreenplayException
    {
        #region Constructors

        /// <summary>
        /// Most basic constructor.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="interaction">The waiting interaction.</param>
        /// <param name="actual">The actual value received after waiting.</param>
        private WaitingException(string message, AbstractWait<TValue> interaction, TValue actual) :
            base(message)
        {
            Interaction = interaction;
            ActualValue = actual;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interaction">The waiting interaction.</param>
        /// <param name="actual">The actual value received after waiting.</param>
        public WaitingException(AbstractWait<TValue> interaction, TValue actual) :
            this($"{interaction} timed out yielding '{actual}'", interaction, actual)
        { }

        #endregion

        #region Properties

        /// <summary>
        /// The actual value received after waiting.
        /// </summary>
        public TValue ActualValue { get; }

        /// <summary>
        /// The waiting interaction.
        /// </summary>
        public AbstractWait<TValue> Interaction { get; }

        #endregion
    }
}